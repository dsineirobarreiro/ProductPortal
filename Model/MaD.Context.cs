﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PracticaMad.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Comentario> Comentario { get; set; }
        public virtual DbSet<Etiqueta> Etiqueta { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Puntuacion> Puntuacion { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Valoracion> Valoracion { get; set; }
    }
}
