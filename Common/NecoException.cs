using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public class NecoException : Exception
    {
        public List<Error> Erros { get; set; }

        public NecoException(List<Error> errors) 
        {
            this.Erros = errors;
        }

        //Códigos gerados pela própria IDE. (Permite o uso de uma exception personalizada)
        public NecoException(string message) : base(message) { }

        public NecoException(string message, Exception inner) : base(message, inner) { }

        protected NecoException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
