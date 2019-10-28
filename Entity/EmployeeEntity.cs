using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace EmployeeRestAdapterService.Entity
{
    public class EmployeeEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public virtual int EMPNO { get; set; }
        public virtual string ENAME { get; set; }
        public virtual string JOB { get; set; }
        public virtual string MGR { get; set; }
        public virtual DateTime HIREDATE { get; set; }
        public virtual string SAL { get; set; }
        public virtual string COMM { get; set; }
        public virtual string DEPTNO { get; set; }
    }
}
