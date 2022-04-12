namespace Service
{
    public static class ResponseFactory
    {
        public static Response SummonResponseDatabaseError()
        {
            return new Response()
            {
                Sucesso = false,
                Mensagem = "Erro no banco de dados, contate o administrador."
            };
        }

        public static Response SummonResponseSuccess()
        {
            return new Response()
            {
                Sucesso = true,
                Mensagem = "Operação realizada com sucesso."
            };
        }

        public static DataResponse<T> SummonResponseDatabaseDataError<T>()
        {
            return new DataResponse<T>()
            {
                Sucesso = false,
                Mensagem = "Erro no banco de dados, contate o administrador."
            };
        }

        public static SingleResponse<T> SummonResponseDatabaseSingleError<T>()
        {
            return new SingleResponse<T>()
            {
                Sucesso = false,
                Mensagem = "Erro no banco de dados, contate o administrador."
            };
        }

        public static Response SummonResponsePersonRegisteredSuccessfully()
        {
            return new Response()
            {
                Sucesso = true,
                Mensagem = "Pessoa cadastrada com sucesso."
            };
        }

        public static Response SummonResponsePersonAlreadyRegisteredError()
        {
            return new Response()
            {
                Sucesso = false,
                Mensagem = "Pessoa já cadastrada."
            };
        }
    }
}
