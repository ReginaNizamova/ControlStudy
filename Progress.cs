//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Authorization
{
    using System;
    using System.Collections.Generic;
    
    public partial class Progress
    {
        public int CodeProgress { get; set; }
        public int CodePerson { get; set; }
        public int CodeDiscipline { get; set; }
        public int Grade { get; set; }
        public System.DateTime DateGrade { get; set; }
    
        public virtual Discipline Discipline { get; set; }
        public virtual Person Person { get; set; }
    }
}
