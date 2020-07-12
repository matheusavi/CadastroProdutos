namespace CadastroProduto.Api.Principal
{
    internal class ErrorModel
    {
        public ErrorModel(string property, string error)
        {
            Property = property;
            Error = error;
        }

        public string Property { get; }
        public string Error { get; }
    }
}
