namespace Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Guia_nueva : DbContext
    {
        public Guia_nueva()
            : base("name=Guia_nueva")
        {
        }

        public virtual DbSet<ActiveSubstances> ActiveSubstances { get; set; }
        public virtual DbSet<Addresses> Addresses { get; set; }
        public virtual DbSet<Advertisements> Advertisements { get; set; }
        public virtual DbSet<AdvertTypes> AdvertTypes { get; set; }
        public virtual DbSet<Alphabet> Alphabet { get; set; }
        public virtual DbSet<AttributeGroup> AttributeGroup { get; set; }
        public virtual DbSet<Attributes> Attributes { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Brands> Brands { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<ClientBrands> ClientBrands { get; set; }
        public virtual DbSet<ClientBrandTypes> ClientBrandTypes { get; set; }
        public virtual DbSet<ClientCategories> ClientCategories { get; set; }
        public virtual DbSet<ClientProductCategories> ClientProductCategories { get; set; }
        public virtual DbSet<ClientProducts> ClientProducts { get; set; }
        public virtual DbSet<ClientProductSubstances> ClientProductSubstances { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<ClientTypes> ClientTypes { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<EditionAttributeGroup> EditionAttributeGroup { get; set; }
        public virtual DbSet<EditionAttributes> EditionAttributes { get; set; }
        public virtual DbSet<EditionClientProducts> EditionClientProducts { get; set; }
        public virtual DbSet<EditionClientProductShots> EditionClientProductShots { get; set; }
        public virtual DbSet<EditionClients> EditionClients { get; set; }
        public virtual DbSet<EditionClientSpecialities> EditionClientSpecialities { get; set; }
        public virtual DbSet<Editions> Editions { get; set; }
        public virtual DbSet<HeterogeneousCategories> HeterogeneousCategories { get; set; }
        public virtual DbSet<MatchCategories> MatchCategories { get; set; }
        public virtual DbSet<ParticipantProducts> ParticipantProducts { get; set; }
        public virtual DbSet<PharmaceuticalForms> PharmaceuticalForms { get; set; }
        public virtual DbSet<PhoneTypes> PhoneTypes { get; set; }
        public virtual DbSet<Presentations> Presentations { get; set; }
        public virtual DbSet<ProductContents> ProductContents { get; set; }
        public virtual DbSet<ProductPharmaForms> ProductPharmaForms { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductSubstances> ProductSubstances { get; set; }
        public virtual DbSet<ProductTypes> ProductTypes { get; set; }
        public virtual DbSet<Specialities> Specialities { get; set; }
        public virtual DbSet<States> States { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActiveSubstances>()
                .Property(e => e.ActiveSubstance)
                .IsUnicode(false);

            modelBuilder.Entity<ActiveSubstances>()
                .HasMany(e => e.ProductSubstances)
                .WithRequired(e => e.ActiveSubstances)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Addresses>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Addresses>()
                .Property(e => e.ZipCode)
                .IsUnicode(false);

            modelBuilder.Entity<Addresses>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Addresses>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Addresses>()
                .Property(e => e.Web)
                .IsUnicode(false);

            modelBuilder.Entity<Addresses>()
                .Property(e => e.Suburb)
                .IsUnicode(false);

            modelBuilder.Entity<Advertisements>()
                .Property(e => e.AdvertName)
                .IsUnicode(false);

            modelBuilder.Entity<Advertisements>()
                .Property(e => e.BaseUrl)
                .IsUnicode(false);

            modelBuilder.Entity<AdvertTypes>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AdvertTypes>()
                .HasMany(e => e.Advertisements)
                .WithRequired(e => e.AdvertTypes)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Alphabet>()
                .Property(e => e.Letter)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AttributeGroup>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AttributeGroup>()
                .Property(e => e.AttributeGroupKey)
                .IsUnicode(false);

            modelBuilder.Entity<Attributes>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Books>()
                .Property(e => e.BookName)
                .IsUnicode(false);

            modelBuilder.Entity<Books>()
                .HasMany(e => e.Editions)
                .WithRequired(e => e.Books)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Brands>()
                .Property(e => e.BrandName)
                .IsUnicode(false);

            modelBuilder.Entity<Brands>()
                .Property(e => e.Logo)
                .IsUnicode(false);

            modelBuilder.Entity<Brands>()
                .Property(e => e.BaseURL)
                .IsUnicode(false);

            modelBuilder.Entity<Brands>()
                .HasMany(e => e.ClientBrands)
                .WithRequired(e => e.Brands)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categories>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Categories>()
                .HasMany(e => e.ClientCategories)
                .WithRequired(e => e.Categories)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categories>()
                .HasMany(e => e.ClientProductCategories)
                .WithRequired(e => e.Categories)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categories>()
                .HasMany(e => e.MatchCategories)
                .WithRequired(e => e.Categories)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categories>()
                .HasOptional(e => e.Categories1)
                .WithRequired(e => e.Categories2);

            modelBuilder.Entity<ClientBrands>()
                .Property(e => e.Page)
                .IsUnicode(false);

            modelBuilder.Entity<ClientBrands>()
                .Property(e => e.ExpireDescription)
                .IsUnicode(false);

            modelBuilder.Entity<ClientBrandTypes>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ClientProductCategories>()
                .HasMany(e => e.EditionClientProducts)
                .WithRequired(e => e.ClientProductCategories)
                .HasForeignKey(e => new { e.ClientId, e.ProductId, e.CategoryId })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClientProducts>()
                .HasMany(e => e.ClientProductCategories)
                .WithRequired(e => e.ClientProducts)
                .HasForeignKey(e => new { e.ClientId, e.ProductId })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClientProducts>()
                .HasMany(e => e.ParticipantProducts)
                .WithRequired(e => e.ClientProducts)
                .HasForeignKey(e => new { e.ClientId, e.ProductId })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClientProducts>()
                .HasMany(e => e.ClientProductSubstances)
                .WithRequired(e => e.ClientProducts)
                .HasForeignKey(e => new { e.ClientId, e.ProductId })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClientProductSubstances>()
                .HasMany(e => e.Editions)
                .WithMany(e => e.ClientProductSubstances)
                .Map(m => m.ToTable("EditionClientMedicalProducts").MapLeftKey(new[] { "ProductId", "PharmaFormId", "ActiveSubstanceId", "PresentationId", "ClientId" }).MapRightKey("EditionId"));

            modelBuilder.Entity<Clients>()
                .Property(e => e.ClientCode)
                .IsUnicode(false);

            modelBuilder.Entity<Clients>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<Clients>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Clients>()
                .Property(e => e.ShortName)
                .IsUnicode(false);

            modelBuilder.Entity<Clients>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Clients>()
                .HasMany(e => e.ClientCategories)
                .WithRequired(e => e.Clients)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clients>()
                .HasOptional(e => e.Clients1)
                .WithRequired(e => e.Clients2);

            modelBuilder.Entity<Clients>()
                .HasMany(e => e.EditionClients)
                .WithRequired(e => e.Clients)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clients>()
                .HasMany(e => e.Addresses)
                .WithMany(e => e.Clients)
                .Map(m => m.ToTable("ClientAddresses").MapLeftKey("ClientId").MapRightKey("AddressId"));

            modelBuilder.Entity<ClientTypes>()
                .Property(e => e.TypeName)
                .IsUnicode(false);

            modelBuilder.Entity<ClientTypes>()
                .HasMany(e => e.EditionClients)
                .WithRequired(e => e.ClientTypes)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Countries>()
                .Property(e => e.CountryName)
                .IsUnicode(false);

            modelBuilder.Entity<Countries>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Countries>()
                .HasMany(e => e.Addresses)
                .WithRequired(e => e.Countries)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Countries>()
                .HasMany(e => e.Clients)
                .WithRequired(e => e.Countries)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Countries>()
                .HasMany(e => e.States)
                .WithRequired(e => e.Countries)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Countries>()
                .HasMany(e => e.Editions)
                .WithRequired(e => e.Countries)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EditionAttributes>()
                .HasMany(e => e.ProductContents)
                .WithRequired(e => e.EditionAttributes)
                .HasForeignKey(e => new { e.AttributeId, e.EditionId })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EditionClientProducts>()
                .Property(e => e.Page)
                .IsUnicode(false);

            modelBuilder.Entity<EditionClientProductShots>()
                .Property(e => e.ProductShot)
                .IsUnicode(false);

            modelBuilder.Entity<EditionClients>()
                .Property(e => e.Page)
                .IsUnicode(false);

            modelBuilder.Entity<EditionClients>()
                .HasMany(e => e.ClientBrands)
                .WithRequired(e => e.EditionClients)
                .HasForeignKey(e => new { e.EditionId, e.ClientId, e.ClientTypeId })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EditionClients>()
                .HasMany(e => e.EditionClientSpecialities)
                .WithRequired(e => e.EditionClients)
                .HasForeignKey(e => new { e.EditionId, e.ClientId, e.ClientTypeId })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EditionClientSpecialities>()
                .HasMany(e => e.Advertisements)
                .WithMany(e => e.EditionClientSpecialities)
                .Map(m => m.ToTable("EditionSpecialityClientAdvers").MapLeftKey(new[] { "EditionId", "ClientId", "SpecialityId", "ClientTypeId" }).MapRightKey("AdvertId"));

            modelBuilder.Entity<Editions>()
                .Property(e => e.ISBN)
                .IsUnicode(false);

            modelBuilder.Entity<Editions>()
                .Property(e => e.BarCode)
                .IsUnicode(false);

            modelBuilder.Entity<Editions>()
                .Property(e => e.EditionCode)
                .IsUnicode(false);

            modelBuilder.Entity<Editions>()
                .HasMany(e => e.EditionClientProducts)
                .WithRequired(e => e.Editions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Editions>()
                .HasMany(e => e.EditionClients)
                .WithRequired(e => e.Editions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Editions>()
                .HasMany(e => e.ParticipantProducts)
                .WithRequired(e => e.Editions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Editions>()
                .HasOptional(e => e.Editions1)
                .WithRequired(e => e.Editions2);

            modelBuilder.Entity<Editions>()
                .HasMany(e => e.ClientCategories)
                .WithMany(e => e.Editions)
                .Map(m => m.ToTable("EditionClientCategories").MapLeftKey("EditionId").MapRightKey(new[] { "ClientId", "CategoryId" }));

            modelBuilder.Entity<HeterogeneousCategories>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<HeterogeneousCategories>()
                .Property(e => e.Tratamiento)
                .IsUnicode(false);

            modelBuilder.Entity<HeterogeneousCategories>()
                .HasMany(e => e.MatchCategories)
                .WithRequired(e => e.HeterogeneousCategories)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HeterogeneousCategories>()
                .HasMany(e => e.Clients)
                .WithMany(e => e.HeterogeneousCategories)
                .Map(m => m.ToTable("ClientHeterogeneousCategories").MapLeftKey("HeterogeneousCategoryId").MapRightKey("ClientId"));

            modelBuilder.Entity<HeterogeneousCategories>()
                .HasMany(e => e.ClientProducts)
                .WithMany(e => e.HeterogeneousCategories)
                .Map(m => m.ToTable("ClientProductHeterogeneousCategories").MapLeftKey("HeterogeneousCategoryId").MapRightKey(new[] { "ClientId", "ProductId" }));

            modelBuilder.Entity<ParticipantProducts>()
                .Property(e => e.HTMLContent)
                .IsUnicode(false);

            modelBuilder.Entity<ParticipantProducts>()
                .Property(e => e.XMLContent)
                .IsUnicode(false);

            modelBuilder.Entity<ParticipantProducts>()
                .Property(e => e.FileName)
                .IsUnicode(false);

            modelBuilder.Entity<ParticipantProducts>()
                .HasMany(e => e.EditionClientProductShots)
                .WithRequired(e => e.ParticipantProducts)
                .HasForeignKey(e => new { e.ClientId, e.ProductId, e.EditionId })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ParticipantProducts>()
                .HasMany(e => e.ProductContents)
                .WithRequired(e => e.ParticipantProducts)
                .HasForeignKey(e => new { e.ClientId, e.ProductId, e.EditionId })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PharmaceuticalForms>()
                .Property(e => e.PharmaForm)
                .IsUnicode(false);

            modelBuilder.Entity<PharmaceuticalForms>()
                .HasMany(e => e.ProductPharmaForms)
                .WithRequired(e => e.PharmaceuticalForms)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhoneTypes>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Presentations>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Presentations>()
                .HasMany(e => e.ProductSubstances)
                .WithRequired(e => e.Presentations)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductContents>()
                .Property(e => e.PlainContent)
                .IsUnicode(false);

            modelBuilder.Entity<ProductContents>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<ProductContents>()
                .Property(e => e.HtmlContent)
                .IsUnicode(false);

            modelBuilder.Entity<ProductPharmaForms>()
                .HasMany(e => e.ProductSubstances)
                .WithRequired(e => e.ProductPharmaForms)
                .HasForeignKey(e => new { e.ProductId, e.PharmaFormId })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.ProductPharmaForms)
                .WithRequired(e => e.Products)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Products>()
                .HasOptional(e => e.Products1)
                .WithRequired(e => e.Products2);

            modelBuilder.Entity<ProductSubstances>()
                .HasMany(e => e.ClientProductSubstances)
                .WithRequired(e => e.ProductSubstances)
                .HasForeignKey(e => new { e.ProductId, e.PharmaFormId, e.ActiveSubstanceId, e.PresentationId })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductTypes>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Specialities>()
                .HasMany(e => e.EditionClientSpecialities)
                .WithRequired(e => e.Specialities)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<States>()
                .Property(e => e.StateName)
                .IsUnicode(false);
        }
    }
}
