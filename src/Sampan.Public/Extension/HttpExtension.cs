using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Sampan.Common.Extension
{
    public static class HttpExtension
    {
        #region Get

        /// <summary>
        /// Get请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(this HttpClient httpClient, string url, object param)
            where T : class
        {
            T result = default(T);
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                StringBuilder requestUri = new StringBuilder(url);

                if (param != null)
                {
                    requestUri.Append("?");
                    foreach (var item in param.GetType().GetProperties())
                    {
                        //过滤一些不必要传递的参数
                        var attributes =
                            item.GetCustomAttributes(typeof(JsonIgnoreAttribute), true)
                                    .FirstOrDefault((x => x is JsonIgnoreAttribute)) is
                                JsonIgnoreAttribute;

                        if (!attributes)
                        {
                            requestUri.Append(item.Name);
                            requestUri.Append("=");
                            requestUri.Append(item.GetValue(param));
                            requestUri.Append("&");
                        }
                    }

                    requestUri.Remove(requestUri.Length - 1, 1);
                }

                var response = await httpClient.GetAsync(requestUri.ToString());
                var res = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<T>(res);
                }
                else
                {
                    //Todo: 错误日志
                    throw new Exception($"文件上传失败");
                }
            }
            catch (Exception ex)
            {
                //LogManagerNlog.LogError(ex,$"GetAsync:出错");
                throw new Exception($"文件上传失败");
            }

            return result;
        }

        #endregion

        #region Post

        /// <summary>
        /// Post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<T> PostObjectAsync<T>(this HttpClient httpClient, string url, object param) where T : class
        {
            T result = default(T);
            try
            {
                var content = JsonConvert.SerializeObject(param);

                HttpContent httpContent = new StringContent(content);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var response = await httpClient.PostAsync(url, httpContent);
                var res = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<T>(res);
                }
                else
                {
                    //Todo: 错误日志 res
                    //LogManagerNlog.LogError($"PostFormFileAsync:{res}");
                    throw new Exception($"文件上传失败");
                }
            }
            catch (Exception ex)
            {
                //LogManagerNlog.LogError(ex, $"PostObjectAsync:出错");
                throw new Exception($"文件上传失败");
            }

            return result;
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public static async Task<List<T>> PostFormFileAsync<T>(this HttpClient httpClient, string url, IFormFileCollection formFile) where T : class
        {
            List<T> result = new List<T>();
            try
            {
               //目前单次上传 不采用批量
                foreach (var file in formFile)
                {
                    using (var memoryStream = file.OpenReadStream())
                    {
                        var formData = new MultipartFormDataContent();
                        var streamContent = new StreamContent(memoryStream, (int)memoryStream.Length);
                        streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                        formData.Add(streamContent, file.ContentType, file.FileName);
                        var response = await httpClient.PostAsync(url, formData);
                        var res = await response.Content.ReadAsStringAsync();
                        if (response.IsSuccessStatusCode)
                        {
                            var model = JsonConvert.DeserializeObject<T>(res);
                            result.Add(model);
                        }
                        else
                        {
                            //Todo: 错误日志 res
                            //LogManagerNlog.LogError($"PostFormFileAsync:{res}");
                            throw new Exception($"文件上传失败:{res}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LogManagerNlog.LogError(ex, $"文件上传失败:PostFormFileAsync");
                throw new Exception($"文件上传失败");
            }
            return result;
        }

            /// <summary>
            /// Post请求字节数据
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="httpClient"></param>
            /// <param name="url"></param>
            /// <param name="param"></param>
            /// <returns></returns>
            public static async Task<byte[]> PostByteAsync(this HttpClient httpClient, string url, object param)
        {
            try
            {
                var content = JsonConvert.SerializeObject(param);
                HttpContent httpContent = new StringContent(content);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var response = await httpClient.PostAsync(url, httpContent);
                var res = await response.Content.ReadAsByteArrayAsync();
                if (response.IsSuccessStatusCode)
                {
                    return res;
                }
                else
                {
                    //LogManagerNlog.LogError($"PostByteAsync:{res}");
                    throw new Exception($"文件上传失败");
                }
            }
            catch (Exception ex)
            {
                //LogManagerNlog.LogError(ex,$"PostByteAsync:{ex.Message}");
                throw new Exception($"文件上传失败");
            }
        }

        #endregion

        #region Put

        /// <summary>
        /// Put请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<T> PutObjectAsync<T>(this HttpClient httpClient, string url, object param) where T : class
        {
            T result = default(T);
            try
            {
                var content = JsonConvert.SerializeObject(param);

                HttpContent httpContent = new StringContent(content);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var response = await httpClient.PutAsync(url, httpContent);
                var res = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<T>(res);
                }
                else
                {
                    //LogManagerNlog.LogError($"PutObjectAsync:{res}");
                    throw new Exception($"文件上传失败");
                }
            }
            catch (Exception ex)
            {
                //LogManagerNlog.LogError($"PutObjectAsync:{ex.Message}");
                throw new Exception($"文件上传失败");
            }

            return result;
        }

        /// <summary>
        /// Put请求字节数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<byte[]> PutByteAsync(this HttpClient httpClient, string url, object param)
        {
            try
            {
                var content = JsonConvert.SerializeObject(param);
                HttpContent httpContent = new StringContent(content);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var response = await httpClient.PutAsync(url, httpContent);
                var res = await response.Content.ReadAsByteArrayAsync();
                if (response.IsSuccessStatusCode)
                {
                    return res;
                }
                else
                {
                    //LogManagerNlog.LogError($"PutByteAsync:{res}");
                    throw new Exception($"文件上传失败");
                }
            }
            catch (Exception ex)
            {
                //LogManagerNlog.LogError($"PutByteAsync:{ex.Message}");
                throw new Exception($"文件上传失败");
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<T> DeleteAsync<T>(this HttpClient httpClient, string url)
            where T : class
        {
            T result = default(T);
            try
            {
                var response = await httpClient.DeleteAsync(url);
                var res = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<T>(res);
                }
                else
                {
                    //LogManagerNlog.LogError($"DeleteAsync:{res}");
                }
            }
            catch (Exception ex)
            {
                //LogManagerNlog.LogError(ex,$"DeleteAsync:{ex.Message}");
            }
            return result;
        }

        /// <summary>
        /// Delete批处理，目前没有好的适合restful风格的实现方案，参数还是包装在httpContent中发送
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<T> DeleteBatchAsync<T>(this HttpClient httpClient, string url, object param) where T : class
        {
            T result = default(T);
            try
            {
                var content = JsonConvert.SerializeObject(param);
                HttpContent httpContent = new StringContent(content);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, url);
                requestMessage.Content = httpContent;
                var response = await httpClient.SendAsync(requestMessage);
                var res = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<T>(res);
                }
                else
                {
                    //LogManagerNlog.LogError($"DeleteBatchAsync:{res}");
                }
            }
            catch (Exception ex)
            {
                //LogManagerNlog.LogError(ex,$"DeleteBatchAsync:{ex.Message}");
            }

            return result;
        }

        #endregion
    }
}
