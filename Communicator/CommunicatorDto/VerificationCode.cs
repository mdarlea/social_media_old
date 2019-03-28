namespace Swaksoft.Application.Communicator.Dto
{
    public class VerificationCode
    {
       public string ToPhoneNumber { get; set; }
       public long MemberNumber { get; set; }
        //Not needed
       public int VerificationCodeLength { get; set; }
    }
}
