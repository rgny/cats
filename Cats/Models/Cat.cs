using System;

namespace Cats.Models
{
    public sealed class Cat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public Byte Age { get; set; }

        public override string ToString()=>$"Cat with name: {Name}, breed: {Breed} and age: {Age}.";
 
    }
}

