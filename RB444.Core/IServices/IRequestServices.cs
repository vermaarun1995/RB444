using System.Threading.Tasks;

namespace RB444.Core.IServices
{
    public interface IRequestServices
    {
        void token(string token);
        TOut Get<TIn, TOut>(string uri);
        Task<TOut> GetAsync<TOut>(string uri);
        TOut Post<TIn, TOut>(string uri, TIn contents);
        Task<TOut> PostAsync<TIn, TOut>(string uri, TIn contents);
        Task<string> GetAsyncnew(string uri);
    }
}
