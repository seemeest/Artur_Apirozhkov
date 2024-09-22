using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet;
using Artur_Apirozhkov;
using VkNet;
using VkNet.Model;
using BenchmarkDotNet.Attributes;
using Artur_Apirozhkov.VkApiCore.Services;

namespace Benchmark
{
    [MemoryDiagnoser]
    [RankColumn]
    public class VkBenchmark
    {
        VkApi api = new VkApi();
        UserService userService;
        VkClient VkClient;
        public VkBenchmark()
        {
            string AccessToken = "vk1.a.yp00MhbuZuKhghuSqH7vz4e4BF5hPVNtfPqJicjw674XGqZlU9qtMpAH5Ni_PNACFpc4WolU2sN5PMcLDxC2-3eSmBV6bi-qUgjOtIQIVCU9jJunksceewEZl7mT_bE4UFgqkEiQLN3fSfHh4CA5oqPfHW-m2tNNzxfECIk05v7GEL-AHCAXXnb526Klr9aksdky-ZQC_UhXgreuBcRqlQ";

            api.Authorize(new ApiAuthParams
            {
                AccessToken = AccessToken
            });
            userService = new(api);
            VkClient = new VkClient(api);
        }

        //[Benchmark]
        //public void commonBenchmark()
        //{
        //    userService.GetUserAsync(236667961);
        //}

        //Runtime = .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI; GC = Concurrent Workstation
        //Mean = 453.236 ms, StdErr = 5.717 ms(1.26%), N = 88, StdDev = 53.628 ms
        //Min = 363.744 ms, Q1 = 421.333 ms, Median = 438.489 ms, Q3 = 473.305 ms, Max = 631.642 ms
        //IQR = 51.971 ms, LowerFence = 343.376 ms, UpperFence = 551.262 ms
        //ConfidenceInterval = [433.766 ms; 472.707 ms] (CI 99.9%), Margin = 19.471 ms(4.30% of Mean)

        //Skewness = 1.42, Kurtosis = 5.09, MValue = 2.85
        [Benchmark]
        public void GetUserProfileBenchmark()
        {
            VkClient.GetUserProfile(236667961);
        }

        //Mean = 459.890 ms, StdErr = 6.481 ms(1.41%), N = 94, StdDev = 62.833 ms
        //Min = 352.493 ms, Q1 = 415.838 ms, Median = 435.458 ms, Q3 = 484.958 ms, Max = 631.870 ms
        //IQR = 69.120 ms, LowerFence = 312.158 ms, UpperFence = 588.638 ms
        //ConfidenceInterval = [437.867 ms; 481.913 ms] (CI 99.9%), Margin = 22.023 ms(4.79% of Mean)
        //Skewness = 1.19, Kurtosis = 3.65, MValue = 2.89

        [Benchmark]
        public void GetUserFriendsBenchmark()
        {
            VkClient.GetUserFriends(236667961);
        }

        [Benchmark]
        public void GetUserPostsBenchmark()
        {
            VkClient.GetUserPosts(236667961);
        }

        [Benchmark]
        public void GetUserGroupsBenchmark()
        {
            VkClient.GetUserGroups(236667961);
        }

        [Benchmark]
        public void GetUserPhotosBenchmark()
        {
            VkClient.GetUserPhotos(236667961);
        }
    }
}
