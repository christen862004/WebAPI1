namespace WebAPI1.DTO
{
    public class EmpWithDeptDTO
    {
        //encrypt column name
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        //Some Fild Not send
        //Get Field from anoth er model(DEpt)
        public string DeptName { get; set; }
    }
}
