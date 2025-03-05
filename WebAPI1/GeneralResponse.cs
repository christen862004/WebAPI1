namespace WebAPI1
{
    public class GeneralResponse
    {
        public bool IsPass { get; set; }
        public dynamic Data { get; set; }  //Real Data (obj | Collection)  -Static Message -Exception -ModelSatate
    }
}
