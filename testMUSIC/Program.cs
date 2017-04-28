using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cma.cimiss.client;
using cma.cimiss;

namespace testMUSIC
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 1. 定义client对象 */
            DataQueryClient client = new DataQueryClient();

            /* 2.   调用方法的参数定义，并赋值 */
            /*   2.1 用户名&密码 */
            String userId = "NMC_YBS_zhoujun";// 
            String pwd = "zhouj123";// 
                                           /*   2.2 接口ID */
            String interfaceId = "getSurfEleByTimeRangeAndStaLevels";
            /*   2.3 接口参数，多个参数间无顺序 */
            Dictionary<String, String> params1 = new Dictionary<String, String>();
            // 必选参数 
            params1.Add("dataCode", "SURF_CHN_MUL_DAY"); // 资料代码 
            params1.Add("elements", "Station_Name," +
                "Station_Id_d," +
                "Year," +
                "Mon," +
                "Day," +
                "PRE_Time_2020," +
                "PRE_Time_0808"
);// 检索要素：站号、小时降水 , DATA_ID, RIV_MS_CODE, AREA_RIVBASIN, POP_RIVBASIN, LENG_RIV, ORISITE, LON_ORI, LAT_ORI, ESTSITE, LON_ESTP, LAT_ESTP, CS_NAME
            params1.Add("timeRange", "[20170101000000,20170110000000]"); // 检索时间 
            params1.Add("staLevels", "011,012,013");
                                                    // 可选参数 
            //params1.Add("orderby", "RECORD_ID:ASC"); // 排序：按照站号从小到大 
                                                        //   params.put("limitCnt", "10") ; //返回最多记录数：10 
                                                        /*   2.4 返回文件的格式 */
            String dataFormat = "text";
            /*   2.5 文件的本地全路径 */
            String savePath = "../../../../data/整理/中国地面日值资料201701.txt";
            /*   2.6 返回文件的描述对象 */
            RetFilesInfo retFilesInfo = new RetFilesInfo();

            /* 3.   调用接口 */
            try
            {
                // 初始化接口服务连接资源 
                client.initResources();
                Console.WriteLine("finally initResources");
                // 调用接口 
                int rst = client.callAPI_to_saveAsFile(userId, pwd, interfaceId,
                        params1, dataFormat, savePath, retFilesInfo);
                // 输出结果 
                if (rst == 0)
                {   // 正常返回 
                    Console.WriteLine(retFilesInfo.fileInfos[0].fileUrl);
                }
                else
                {   // 异常返回 
                    Console.WriteLine("[error] StaElemSearchAPI_CLIB_callAPI_to_saveAsFile_XML.");
                    Console.WriteLine("\treturn code: {0}. \n", rst);
                    Console.WriteLine("\terror message: {0}.\n",
                             retFilesInfo.request.errorMessage);
                }
            }
            catch (Exception e)
            {
                // 异常输出 
                Console.WriteLine(e.Message);
                //e.Message();
            }
            finally
            {
                // 释放接口服务连接资源 
                client.destroyResources();
            }
        }
    }
}
