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
    
    public partial class Puntuacion
    {
        public long userId { get; set; }
        public double puntuacion1 { get; set; }
        public Nullable<int> numValoraciones { get; set; }
    
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): FK_PuntuacionUsuario
        /// </summary>
        public virtual Usuario Usuario { get; set; }
    
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
    
    			hash = hash * multiplier + puntuacion1.GetHashCode();
    			hash = hash * multiplier + (numValoraciones == null ? 0 : numValoraciones.GetHashCode());
    
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
    	    
            Puntuacion target = obj as Puntuacion;
    
    		return true
               &&  (this.userId == target.userId )       
               &&  (this.puntuacion1 == target.puntuacion1 )       
               &&  (this.numValoraciones == target.numValoraciones )       
               ;
    
        }
    
    
    	public static bool operator ==(Puntuacion  objA, Puntuacion  objB)
        {
            // Check if the objets are the same Puntuacion entity
            if(Object.ReferenceEquals(objA, objB))
                return true;
      
            return objA.Equals(objB);
    }
    
    
    	public static bool operator !=(Puntuacion  objA, Puntuacion  objB)
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
    	    StringBuilder strPuntuacion = new StringBuilder();
    
    		strPuntuacion.Append("[ ");
           strPuntuacion.Append(" userId = " + userId + " | " );       
           strPuntuacion.Append(" puntuacion1 = " + puntuacion1 + " | " );       
           strPuntuacion.Append(" numValoraciones = " + numValoraciones + " | " );       
            strPuntuacion.Append("] ");    
    
    		return strPuntuacion.ToString();
        }
    
    
    }
}
