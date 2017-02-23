namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using Model;

    public partial class Products
    {

        Guia_nueva db = new Guia_nueva();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            ProductPharmaForms = new HashSet<ProductPharmaForms>();
        }

        [Key]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        public bool Active { get; set; }

        public int? ParentId { get; set; }

        public byte? TypeId { get; set; }

        public byte? AlphabetId { get; set; }

        public int? ProductId_Anterior { get; set; }

        public virtual Alphabet Alphabet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPharmaForms> ProductPharmaForms { get; set; }

        public virtual Products Products1 { get; set; }

        public virtual Products Products2 { get; set; }

        public virtual ProductTypes ProductTypes { get; set; }
        
        //Definición de metodo
        public List<Products> Listar()
        {
            var productos = new List<Products>();
            try
            {

                productos = db.Products.ToList();
            
            }
            catch (Exception)
            {
                // Throw; envia una señal de excepción al interprete
                throw;
            }
            return productos;
        }

    }
}