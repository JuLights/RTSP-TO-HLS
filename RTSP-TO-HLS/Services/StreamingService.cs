using FFMpegCore;
using FFMpegCore.Enums;

namespace RTSP_TO_HLS.Services
{
    public class StreamingService
    {
        public StreamingService()
        {

        }
        public async Task Streamer(string url)
        {

            var link = new Uri(url);

            await FFMpegArguments
        .FromUrlInput(link)
        .OutputToFile("_playlist_1080p.m3u8", false, options => options
            .WithVideoCodec("copy")
            .WithConstantRateFactor(21)
            .WithAudioCodec(AudioCodec.Aac).WithCustomArgument("-f hls -hls_time 3 -hls_list_size 5 -hls_flags delete_segments+split_by_time+append_list _playlist_1080p.m3u8")
            //.WithCustomArgument("delete_segments")
            .WithVariableBitrate(2)
            .UsingMultithreading(true)
            //.WithVideoFilters(filterOptions => filterOptions
            //    .Scale(VideoSize.FullHd))
            .WithFastStart())
        .ProcessAsynchronously();
        }
    }
}
