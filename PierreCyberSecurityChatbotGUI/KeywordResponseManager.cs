using System;
using System.Collections.Generic;

namespace PierreCyberSecurityBotPROG
{
    public class KeywordResponse<T>
    {
        public T Keyword { get; set; }
        public string Response { get; set; }
    }

    public class KeywordResponseManager
    {
        private readonly List<KeywordResponse<string>> _responses;

        public KeywordResponseManager(List<KeywordResponse<string>> responses)
        {
            _responses = responses;
        }

        public bool TryGetResponse(string input, out string response)
        {
            foreach (var item in _responses)
            {
                
                if (input.IndexOf(item.Keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    response = item.Response;
                    return true;
                }
            }

            response = null;
            return false;
        }
    }
}
