//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace encuestas.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class encuesta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public encuesta()
        {
            this.detalle_encuesta = new HashSet<detalle_encuesta>();
        }
    
        public int idencuesta { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<bool> Estado { get; set; }
        public Nullable<int> idusuario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalle_encuesta> detalle_encuesta { get; set; }
        public virtual usuario usuario { get; set; }
    }
}
