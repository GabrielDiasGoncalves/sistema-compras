using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public NomeFornecedor NomeFornecedor { get; private set; }
        public CondicaoPagamento Condicao { get; private set; }
        public IList<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        public Money TotalGeral { get; private set; }
        public Situacao Situacao { get; private set; }

        private SolicitacaoCompra() : base() { }

        public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor)
            : base()
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            Itens.Add(new Item(produto, qtde));
        }

        public void RegistrarCompra(IEnumerable<Item> itens)
        {
            if (!ExisteItensNaCompra(itens))
                return;

            var totalDaCompra = RecuperarTotalDaCompra(itens);
            
            Condicao = new CondicaoPagamento(totalDaCompra);
            TotalGeral = new Money(totalDaCompra);
        }

        public bool ExisteItensNaCompra(IEnumerable<Item> itens)
        {
            return itens?.Count() > 0;
        }

        public decimal RecuperarTotalDaCompra(IEnumerable<Item> itens)
        {
            return itens.Sum(x => x.Subtotal.Value);
        }
    }
}