using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Bomeans.IRNet;

namespace BomeansPCTool
{
    public class BMSFile
    {
        private Dictionary<String, MyReaderMatchResult> mResultList;

        public BMSFile()
        {
            mResultList = null;
        }

        public BMSFile(Dictionary<String, MyReaderMatchResult> resultList)
        {
            mResultList = resultList;
        }

        public Dictionary<String, MyReaderMatchResult> GetLearningResult()
        {
            return mResultList;
        }

        public Boolean Load(String filePath)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }

            if (null == mResultList)
            {
                mResultList = new Dictionary<String, MyReaderMatchResult>();
            }
            else
            {
                mResultList.Clear();
            }

            Dictionary<String, MyReaderMatchResult> resultMap = new Dictionary<String, MyReaderMatchResult>();

            try
            {
                using (StreamReader file = File.OpenText(filePath))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject jRoot = (JObject)JToken.ReadFrom(reader);

                        JArray jResults = (JArray)jRoot["learning_results"];

                        MyReaderMatchResult matchResult;
                        JObject tmpJObject = new JObject();
                        for (int idx = 0; idx < jResults.Count; idx++)
                        {
                            matchResult = new MyReaderMatchResult();

                            try
                            {
                                JObject jRemoteKey = (JObject)jResults[idx];

                                String keyId = (String)jRemoteKey["key_id"];

                                if (null != jRemoteKey["raw_learning_data"])
                                {
                                    List<byte> byteDataList = new List<byte>();
                                    String rawDataString = (String)jRemoteKey["raw_learning_data"];
                                    String[] byteStirngArray = rawDataString.Split(new char[] { ',' });
                                    for (int i = 0; i < byteStirngArray.Length; i++)
                                    {
                                        if (byteStirngArray[i].Length == 0)
                                        {
                                            continue;
                                        }

                                        if (byteStirngArray[i].StartsWith("0x") || byteStirngArray[i].StartsWith("0X"))
                                        {
                                            byteDataList.Add(Convert.ToByte(byteStirngArray[i].Substring(2), 16));
                                        }
                                        else
                                        {
                                            byteDataList.Add(Convert.ToByte(byteStirngArray[i], 16));
                                        }
                                    }

                                    matchResult.RawLearningData = byteDataList.ToArray();
                                }
                                else
                                {
                                    matchResult.RawLearningData = null;
                                }

                                // match result
                                JObject jMatchResult = (JObject)jRemoteKey["match_result"];
                                if (null == jMatchResult)
                                {
                                    matchResult.MatchResult = null;
                                }
                                else
                                {
                                    String formatId = "";
                                    long customCode = 0;
                                    long keyCode = 0;

                                    if (jMatchResult["format_id"] != null)
                                    {
                                        formatId = (String)jMatchResult["format_id"];
                                    }
                                    if (jMatchResult["custom_code"] != null)
                                    {
                                        customCode = GetInt64FromString((String)jMatchResult["custom_code"]);
                                    }
                                    if (jMatchResult["key_code"] != null)
                                    {
                                        customCode = GetInt64FromString((String)jMatchResult["key_code"]);
                                    }

                                    matchResult.MatchResult = new ReaderMatchResult(formatId, customCode, keyCode);
                                }

                                resultMap.Add(keyId, matchResult);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            mResultList = resultMap;
            return true;
        }

        private long GetInt64FromString(String int64String)
        {
            try
            {
                if (int64String.StartsWith("0x") || int64String.StartsWith("0X"))
                {
                    return Convert.ToInt64(int64String.Substring(2), 16);
                }
                else
                {
                    return Convert.ToInt64(int64String);
                }
            }
            catch
            {
                return 0;
            }
        }

        public Boolean Save(String filePath)
        {
            if (null == mResultList)
            {
                return false;
            }

            return Save(mResultList, filePath);
        }

        private Boolean Save(Dictionary<String, MyReaderMatchResult> resultList, String filePath)
        {
            try
            {
                JObject jResults = GetJson(resultList);

                using (StreamWriter sw = File.CreateText(filePath))
                {
                    using (JsonWriter jw = new JsonTextWriter(sw))
                    {
                        jw.Formatting = Formatting.Indented;

                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(jw, jResults);

                        return true;
                    }
                }
            }
            catch
            {
            }

            return false;
        }

        private JObject GetJson(Dictionary<String, MyReaderMatchResult> resultList)
        {
            JObject jRoot = new JObject();

            JArray jArray = new JArray();
            foreach (KeyValuePair<String, MyReaderMatchResult> entry in resultList)
            {
                jArray.Add(GetLearningResultJson(entry));
            }

            jRoot.Add("learning_results", jArray);

            return jRoot;
        }

        private JObject GetLearningResultJson(KeyValuePair<String, MyReaderMatchResult> result)
        {
            JObject jResult = new JObject();
            jResult.Add("key_id", result.Key);  // key id

            if (null != result.Value)
            {
                MyReaderMatchResult matchResult = result.Value;

                if (matchResult.MatchResult != null)
                {
                    JObject jMatchedResult = new JObject();
                    jMatchedResult.Add("format_id", matchResult.MatchResult.formatId);
                    jMatchedResult.Add("custom_code", String.Format("0x{0:X}", matchResult.MatchResult.customCode));
                    jMatchedResult.Add("key_code", String.Format("0x{0:X}", matchResult.MatchResult.keyCode));

                    jResult.Add("match_result", jMatchedResult);
                }

                if (matchResult.RawLearningData != null)
                {
                    String rawDataString = "";
                    foreach (byte byteData in matchResult.RawLearningData)
                    {
                        rawDataString += String.Format("0x{0:X},", byteData);
                    }
                    //remove the tailing comma
                    rawDataString = rawDataString.TrimEnd(new char[] { ',' });

                    jResult.Add("raw_learning_data", rawDataString);
                }
            }

            return jResult;
        }
    }
}
