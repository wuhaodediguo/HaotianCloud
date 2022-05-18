using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaotianCloud.Domain.Entity.VehicleManage
{
    public class UpDto
    {
        public List<EncodeDto> encode { get; set; }
        public List<ipinfoDto> ipinfo { get; set; }
    }

    public class EncodeDto
    {
        public List<venc_param> venc_param { get; set; }
        public List<aenc_param> aenc_param { get; set; }
        public List<vdec_param> vdec_param { get; set; }
        public string analogMode { get; set; }
        public string display_channel { get; set; }
        public string jt_Terid { get; set; }
        public string jt_server { get; set; }
        public string jt_tcpport { get; set; }
        public string vdec_outmux { get; set; }
       
        public string viMode { get; set; }
        public List<vi_param> vi_param { get; set; }
      

    }

    public class venc_param
    {
        public string audioEnable { get; set; }
        public string http_flv_enable { get; set; }
        public string http_flv_uri { get; set; }
        public string http_hls_enable { get; set; }
        public string http_hls_uri { get; set; }
        public string http_ts_enable { get; set; }
        public string http_ts_uri { get; set; }
        public string live_enable { get; set; }
        public string live_publish_url { get; set; }
        public string multicast_enable { get; set; }
        public string multicast_ip { get; set; }
        public string multicast_port { get; set; }
        public string recordEnable { get; set; }
        public int rtmp_enable { get; set; }
        public string rtmp_publish_auto_stop { get; set; }
        public string rtmp_publish_enable_write { get; set; }
        public string rtmp_publish_stop_port { get; set; }
        public string rtmp_publish_stop_url { get; set; }
        public string rtmp_publish_uri { get; set; }
        public int rtsp_enable { get; set; }
        public string rtsp_password { get; set; }
        public string rtsp_uri { get; set; }
        public string rtsp_username { get; set; }
        public string transcodeEnable { get; set; }
        public string ts_pid { get; set; }
        public string venc_codec { get; set; }

        public string vencFramerate { get; set; }
        public string subtype { get; set; }
        public string audio_source_url { get; set; }
        public string compact_mode { get; set; }
        public string forceAudioTranscode { get; set; }
        public string verticalEnable { get; set; }
        public List<osd> osd { get; set; }

    }


    public class osd
    {
        public string alpha { get; set; }
        public string bcolor { get; set; }
        public string enable { get; set; }
        public string font_color { get; set; }
        public string font_size { get; set; }
        public string osd_type { get; set; }
        public string speed { get; set; }
        public string txt { get; set; }
        public string x { get; set; }
        public string y { get; set; }

    }

    public class aenc_param
    {
        public int aenc_bitrate { get; set; }
        public int aenc_code { get; set; }
        public int aenc_input { get; set; }
        public int aenc_samplerate { get; set; }
        public int analog_vol { get; set; }
        public int rtsp_g711 { get; set; }
       
    }

    public class vdec_param
    {
        public string vdec_enable  { get ; set; }
        public string video_url { get; set; }
       
    }

    public class sysuser
    {
        public string soft_version { get; set; }
        public string user_name { get; set; }
        public string user_password { get; set; }

    }

    public class transerial
    {
        public int databits { get; set; }
        public int flow_control_enable { get; set; }
        public int parity { get; set; }
        public int port { get; set; }
        public int serial_baundrate { get; set; }
        public string server_ip { get; set; }
        public int stopbits { get; set; }
        public int tcp_client_mode { get; set; }

    }

    public class vi_param
    {
        public string autoFocusEnable { get; set; }
        public string brightness { get; set; }
        public string contrast { get; set; }
        public string hue { get; set; }
        public string interleaced { get; set; }
        public string ir_status { get; set; }
        public string lua { get; set; }
        public string mirror { get; set; }
        public string rotate { get; set; }
        public string saturation { get; set; }

    }

    public class ipinfoDto
    {
        public eth_net eth_net { get; set; }
        public local_net local_net { get; set; }
        
    }

    public class eth_net
    {
        public string ip { get; set; }
        public string gateway { get; set; }
        public string dns1 { get; set; }
        public string dns0 { get; set; }

    }

    public class local_net
    {
        public int dhcp_enable { get; set; }
        public string ip { get; set; }
        public string mac { get; set; }
        public string netmask { get; set; }

    }




}
