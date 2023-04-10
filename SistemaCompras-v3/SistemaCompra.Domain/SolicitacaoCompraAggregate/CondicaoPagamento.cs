using SistemaCompra.Domain.Core;
using System.Collections.Generic;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class CondicaoPagamento
    {
        private const decimal _TOTAL_COMPRA_PARA_SER_CONDICAO_30 = 50000;
        private IList<int> _valoresPossiveis = new List<int>() { 0, 30, 60, 90 };
        public int Valor { get; private set; }

        private CondicaoPagamento(){}

        public CondicaoPagamento(int condicao)
        {
            if (!_valoresPossiveis.Contains(condicao)) 
                throw new BusinessRuleException("Condição de pagamento deve ser " +_valoresPossiveis.ToString());

            Valor = condicao;
        }

        public CondicaoPagamento(decimal totalCompra)
        {
            if (totalCompra > _TOTAL_COMPRA_PARA_SER_CONDICAO_30)
                Valor = 30;
        }
    }
}