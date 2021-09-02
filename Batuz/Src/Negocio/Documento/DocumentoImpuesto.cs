namespace Batuz.Negocio.Documento
{

    /// <summary>
    /// Representa una línea de impuestos en un documento.
    /// </summary>
    public class DocumentoImpuesto
    {

        #region Propiedades Públicas de Instancia

        /// <summary>
        /// Código de impuesto del impuesto.
        /// </summary>
        public string IdentificadorImpuestos { get; set; }

        /// <summary>
        /// Tipo impositivo impuestos soportados.
        /// </summary>
        public decimal BaseImpuestos { get; set; }

        /// <summary>
        /// Tipo impositivo impuestos retenidos.
        /// </summary>
        public decimal TipoImpuestos { get; set; }

        /// <summary>
        /// Tipo impositivo impuestos retenidos.
        /// </summary>
        public decimal CuotaImpuestos { get; set; }

        #endregion

        #region Métodos Públicos de Instancia

        /// <summary>
        /// Representación textual de la instancia.
        /// </summary>
        /// <returns>Representación textual de la instancia.</returns>
        public override string ToString()
        {
            return $"({IdentificadorImpuestos}) {BaseImpuestos} X {TipoImpuestos}% = {CuotaImpuestos}";
        }

        #endregion

    }
}
