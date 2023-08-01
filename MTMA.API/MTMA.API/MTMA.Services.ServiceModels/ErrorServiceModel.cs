namespace MTMA.Services.ServiceModels
{
    public class ErrorServiceModel
    {
        required public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);
    }
}
