//------------------------------------------------------------------------------
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
    using System.Text;
    using System.Collections.Generic;
    
    public partial class Comentario
    {
        public Comentario()
        {
            this.Etiqueta = new HashSet<Etiqueta>();
        }
    
        public long comID { get; set; }
        public long usuario { get; set; }
        public long producto { get; set; }
        public System.DateTime date { get; set; }
        public string text { get; set; }
    
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): FK_ComentarioProducto
        /// </summary>
        public virtual Producto Producto1 { get; set; }
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): FK_ComentarioUsuario
        /// </summary>
        public virtual Usuario Usuario1 { get; set; }
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): TagCom
        /// </summary>
        public virtual ICollection<Etiqueta> Etiqueta { get; set; }
    
    	/// <summary>
    	/// A hash code for this instance, suitable for use in hashing algorithms and data structures 
    	/// like a hash table. It uses the Josh Bloch implementation from "Effective Java"
        /// Primary key of entity is not included in the hash calculation to avoid errors
    	/// with Entity Framework creation of key values.
    	/// </summary>
    	/// <returns>
    	/// Returns a hash code for this instance.
    	/// </returns>
    	public override int GetHashCode()
    	{
    	    unchecked
    	    {
    			int multiplier = 31;
    			int hash = GetType().GetHashCode();
    
    			hash = hash * multiplier + usuario.GetHashCode();
    			hash = hash * multiplier + producto.GetHashCode();
    			hash = hash * multiplier + date.GetHashCode();
    			hash = hash * multiplier + (text == null ? 0 : text.GetHashCode());
    
    			return hash;
    	    }
    
    	}
        
        /// <summary>
        /// Compare this object against another instance using a value approach (field-by-field) 
        /// </summary>
        /// <remarks>See http://www.loganfranken.com/blog/687/overriding-equals-in-c-part-1/ for detailed info </remarks>
    	public override bool Equals(object obj)
    	{
    
            if (ReferenceEquals(null, obj)) return false;        // Is Null?
            if (ReferenceEquals(this, obj)) return true;         // Is same object?
            if (obj.GetType() != this.GetType()) return false;   // Is same type?
    	    
            Comentario target = obj as Comentario;
    
    		return true
               &&  (this.comID == target.comID )       
               &&  (this.usuario == target.usuario )       
               &&  (this.producto == target.producto )       
               &&  (this.date == target.date )       
               &&  (this.text == target.text )       
               ;
    
        }
    
    
    	public static bool operator ==(Comentario  objA, Comentario  objB)
        {
            // Check if the objets are the same Comentario entity
            if(Object.ReferenceEquals(objA, objB))
                return true;
      
            return objA.Equals(objB);
    }
    
    
    	public static bool operator !=(Comentario  objA, Comentario  objB)
        {
            return !(objA == objB);
        }
    
    
        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the 
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current 
        /// <see cref="T:System.Object"></see>.
        /// </returns>
    	public override String ToString()
    	{
    	    StringBuilder strComentario = new StringBuilder();
    
    		strComentario.Append("[ ");
           strComentario.Append(" comID = " + comID + " | " );       
           strComentario.Append(" usuario = " + usuario + " | " );       
           strComentario.Append(" producto = " + producto + " | " );       
           strComentario.Append(" date = " + date + " | " );       
           strComentario.Append(" text = " + text + " | " );       
            strComentario.Append("] ");    
    
    		return strComentario.ToString();
        }
    
    
    }
}
