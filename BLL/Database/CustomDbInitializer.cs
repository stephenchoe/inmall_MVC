using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Xml;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using Model;
using DAL;
using Core;
using Core.Extensions;
using Web.Core;


namespace BLL
{
    public class DropCreateAlwaysInitializer<T> : DropCreateDatabaseAlways<InmallContext>
    {
        private readonly AppUserManager userManager;
        private readonly AppRoleManager roleManager;
        public DropCreateAlwaysInitializer(AppUserManager userManager, AppRoleManager roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        protected override void Seed(InmallContext context)
        {
            SeedDataInitializer.InsertCities(context);
            //SeedDataInitializer.CreateRoles(context, this.roleManager);
            //SeedDataInitializer.CreateBoss(context, this.userManager);

            base.Seed(context);
        }


    }


    public class SeedDataInitializer
    {
        public static void InsertCities(InmallContext context)
        {
            List<City> allCity = new List<City>();

            allCity.Add(new City()
            {
                Code = "A",
                Name = "台北市",
                TelCode = "02",
                Districts = new List<District>() 
                { 
                    new District(){ Name="中正區",ZipCode="100"},
                    new District(){ Name="大同區",ZipCode="103"},
                    new District(){ Name="中山區",ZipCode="104"},
                      new District(){ Name="松山區",ZipCode="105"},
                    new District(){ Name="大安區",ZipCode="106"},
                    new District(){ Name="萬華區",ZipCode="108"},
                      new District(){ Name="信義區",ZipCode="110"},
                    new District(){ Name="士林區",ZipCode="111"},
                    new District(){ Name="北投區",ZipCode="112"},
                      new District(){ Name="內湖區",ZipCode="114"},
                    new District(){ Name="南港區",ZipCode="115"},
                    new District(){ Name="文山區",ZipCode="116"},
                }
            });
            allCity.Add(new City()
            {
                Code = "C",
                Name = "基隆市",
                TelCode = "02",
                Districts = new List<District>()
                { 
                    new District(){ Name="仁愛區",ZipCode="200"},
                    new District(){ Name="信義區",ZipCode="201"},
                       new District(){ Name="中正區",ZipCode="202"},
                          new District(){ Name="中山區",ZipCode="203"},
                             new District(){ Name="安樂區",ZipCode="204"},
                                new District(){ Name="暖暖區",ZipCode="205"},
                                   new District(){ Name="七堵區",ZipCode="206"},
                }
            });
            allCity.Add(new City()
            {
                Code = "F",
                Name = "新北市",
                TelCode = "02",
                Districts = new List<District>() 
                { 
                     new District(){Name="萬里區" , ZipCode="207" },
                    new District(){ Name="金山區" , ZipCode="208" },
                    new District(){ Name="板橋區" , ZipCode="220" },
                    new District(){ Name="汐止區" , ZipCode="221" },
                    new District(){ Name="深坑區" , ZipCode="222" },
                    new District(){ Name="石碇區" , ZipCode="223" },
                    new District(){ Name="瑞芳區" , ZipCode="224" },
                    new District(){ Name="平溪區" , ZipCode="226" },
                    new District(){ Name="雙溪區" , ZipCode="227" },
                    new District(){ Name="貢寮區" , ZipCode="228" },
                    new District(){ Name="新店區" , ZipCode="231" },
                    new District(){ Name="坪林區" , ZipCode="232" },
                    new District(){ Name="烏來區" , ZipCode="233" },
                    new District(){ Name="永和區" , ZipCode="234" },
                    new District(){ Name="中和區" , ZipCode="235" },
                    new District(){ Name="土城區" , ZipCode="236" },
                    new District(){ Name="三峽區" , ZipCode="237" },
                    new District(){ Name="樹林區" , ZipCode="238" },
                    new District(){ Name="鶯歌區" , ZipCode="239" },
                    new District(){ Name="三重區" , ZipCode="241" },
                    new District(){ Name="新莊區" , ZipCode="242" },
                    new District(){ Name="泰山區" , ZipCode="243" },
                    new District(){ Name="林口區" , ZipCode="244" },
                    new District(){ Name="蘆洲區" , ZipCode="247" },
                    new District(){ Name="五股區" , ZipCode="248" },
                    new District(){ Name="八里區" , ZipCode="249" },
                    new District(){ Name="淡水區" , ZipCode="251" },
                    new District(){ Name="三芝區" , ZipCode="252" },
                    new District(){ Name="石門區" , ZipCode="253" },
                }
            });

            allCity.Add(new City()
            {
                Code = "G",
                Name = "宜蘭縣",
                TelCode = "03",
                Districts = new List<District>() 
                { 
                       new District(){ Name="宜蘭市" , ZipCode="260" },                  
                        new District(){ Name="頭城鎮" , ZipCode="261"  },
                        new District(){ Name="礁溪鄉" , ZipCode="262"  },
                        new District(){ Name="壯圍鄉" , ZipCode="263"  },
                        new District(){ Name="員山鄉" , ZipCode="264"  },
                        new District(){ Name="羅東鎮" , ZipCode="265"  },
                        new District(){ Name="三星鄉" , ZipCode="266"  },
                        new District(){ Name="大同鄉" , ZipCode="267"  },
                        new District(){ Name="五結鄉" , ZipCode="268"  },
                        new District(){ Name="冬山鄉" , ZipCode="269"  },
                        new District(){ Name="蘇澳鎮" , ZipCode="270"  },
                        new District(){ Name="南澳鄉" , ZipCode="272"  },
                }
            });

            allCity.Add(new City()
            {
                Code = "O",
                Name = "新竹市",
                TelCode = "03",
                Districts = new List<District>() 
                { 
                       new District(){ Name="東區" , ZipCode="300" },                  
                        new District(){ Name="北區" , ZipCode="300"  },
                        new District(){ Name="香山區" , ZipCode="300"  },
                     
                }
            });

            allCity.Add(new City()
            {
                Code = "J",
                Name = "新竹縣",
                TelCode = "03",
                Districts = new List<District>() 
                { 
                        new District{ Name="竹北市" , ZipCode="302" },
                        new District{ Name="湖口鄉" , ZipCode="303" },
                        new District{ Name="新豐鄉" , ZipCode="304" },
                        new District{ Name="新埔鎮" , ZipCode="305" },
                        new District{ Name="關西鎮" , ZipCode="306" },
                        new District{ Name="芎林鄉" , ZipCode="307" },
                        new District{ Name="寶山鄉" , ZipCode="308" },
                        new District{ Name="竹東鎮" , ZipCode="310" },
                        new District{ Name="五峰鄉" , ZipCode="311" },
                        new District{ Name="橫山鄉" , ZipCode="312" },
                        new District{ Name="尖石鄉" , ZipCode="313" },
                        new District{ Name="北埔鄉" , ZipCode="314" },
                        new District{ Name="峨眉鄉" , ZipCode="315" },
                     
                }
            });

            allCity.Add(new City()
            {
                Code = "H",
                Name = "桃園縣",
                TelCode = "03",
                Districts = new List<District>() 
                { 
                          new District{ Name="中壢市" , ZipCode="320" },
                        new District{ Name="平鎮市" , ZipCode="324" },
                        new District{ Name="龍潭鄉" , ZipCode="325" },
                        new District{ Name="楊梅鎮" , ZipCode="326" },
                        new District{ Name="新屋鄉" , ZipCode="327" },
                        new District{ Name="觀音鄉" , ZipCode="328" },
                        new District{ Name="桃園市" , ZipCode="330" },
                        new District{ Name="龜山鄉" , ZipCode="333" },
                        new District{ Name="八德市" , ZipCode="334" },
                        new District{ Name="大溪鎮" , ZipCode="335" },
                        new District{ Name="復興鄉" , ZipCode="336" },
                        new District{ Name="大園鄉" , ZipCode="337" },
                        new District{ Name="蘆竹鄉" , ZipCode="338" },
                     
                }
            });

            allCity.Add(new City()
            {
                Code = "K",
                Name = "苗栗縣",
                TelCode = "037",
                Districts = new List<District>() 
                { 
                         new District{ Name="竹南鎮" , ZipCode="350" },
                        new District{ Name="頭份鎮" , ZipCode="351" },
                        new District{ Name="三灣鄉" , ZipCode="352" },
                        new District{ Name="南庄鄉" , ZipCode="353" },
                        new District{ Name="獅潭鄉" , ZipCode="354" },
                        new District{ Name="後龍鎮" , ZipCode="356" },
                        new District{ Name="通霄鎮" , ZipCode="357" },
                        new District{ Name="苑裡鎮" , ZipCode="358" },
                        new District{ Name="苗栗市" , ZipCode="360" },
                        new District{ Name="造橋鄉" , ZipCode="361" },
                        new District{ Name="頭屋鄉" , ZipCode="362" },
                        new District{ Name="公館鄉" , ZipCode="363" },
                        new District{ Name="大湖鄉" , ZipCode="364" },
                        new District{ Name="泰安鄉" , ZipCode="365" },
                        new District{ Name="銅鑼鄉" , ZipCode="366" },
                        new District{ Name="三義鄉" , ZipCode="367" },
                        new District{ Name="西湖鄉" , ZipCode="368" },
                        new District{ Name="卓蘭鎮" , ZipCode="369" },
                     
                }
            });

            allCity.Add(new City()
            {
                Code = "B",
                Name = "臺中市",
                TelCode = "04",
                Districts = new List<District>() 
                { 
                       new District{ Name="中區" , ZipCode="400" },
                        new District{ Name="東區" , ZipCode="401" },
                        new District{ Name="南區" , ZipCode="402" },
                        new District{ Name="西區" , ZipCode="403" },
                        new District{ Name="北區" , ZipCode="404" },
                        new District{ Name="北屯區" , ZipCode="406" },
                        new District{ Name="西屯區" , ZipCode="407" },
                        new District{ Name="南屯區" , ZipCode="408" },
                        new District{ Name="太平區" , ZipCode="411" },
                        new District{ Name="大里區" , ZipCode="412" },
                        new District{ Name="霧峰區" , ZipCode="413" },
                        new District{ Name="烏日區" , ZipCode="414" },
                        new District{ Name="豐原區" , ZipCode="420" },
                        new District{ Name="后里區" , ZipCode="421" },
                        new District{ Name="石岡區" , ZipCode="422" },
                        new District{ Name="東勢區" , ZipCode="423" },
                        new District{ Name="和平區" , ZipCode="424" },
                        new District{ Name="新社區" , ZipCode="426" },
                        new District{ Name="潭子區" , ZipCode="427" },
                        new District{ Name="大雅區" , ZipCode="428" },
                        new District{ Name="神岡區" , ZipCode="429" },
                        new District{ Name="大肚區" , ZipCode="432" },
                        new District{ Name="沙鹿區" , ZipCode="433" },
                        new District{ Name="龍井區" , ZipCode="434" },
                        new District{ Name="梧棲區" , ZipCode="435" },
                        new District{ Name="清水區" , ZipCode="436" },
                        new District{ Name="大甲區" , ZipCode="437" },
                        new District{ Name="外埔區" , ZipCode="438" },
                        new District{ Name="大安區" , ZipCode="439" },
                     
                }
            });

            allCity.Add(new City()
            {
                Code = "N",
                Name = "彰化縣",
                TelCode = "04",
                Districts = new List<District>() 
                { 
                         new District{ Name="彰化市" , ZipCode="500" },
                        new District{ Name="芬園鄉" , ZipCode="502" },
                        new District{ Name="花壇鄉" , ZipCode="503" },
                        new District{ Name="秀水鄉" , ZipCode="504" },
                        new District{ Name="鹿港鎮" , ZipCode="505" },
                        new District{ Name="福興鄉" , ZipCode="506" },
                        new District{ Name="線西鄉" , ZipCode="507" },
                        new District{ Name="和美鎮" , ZipCode="508" },
                        new District{ Name="伸港鄉" , ZipCode="509" },
                        new District{ Name="員林鎮" , ZipCode="510" },
                        new District{ Name="社頭鄉" , ZipCode="511" },
                        new District{ Name="永靖鄉" , ZipCode="512" },
                        new District{ Name="埔心鄉" , ZipCode="513" },
                        new District{ Name="溪湖鎮" , ZipCode="514" },
                        new District{ Name="大村鄉" , ZipCode="515" },
                        new District{ Name="埔鹽鄉" , ZipCode="516" },
                        new District{ Name="田中鎮" , ZipCode="520" },
                        new District{ Name="北斗鎮" , ZipCode="521" },
                        new District{ Name="田尾鄉" , ZipCode="522" },
                        new District{ Name="埤頭鄉" , ZipCode="523" },
                        new District{ Name="溪州鄉" , ZipCode="524" },
                        new District{ Name="竹塘鄉" , ZipCode="525" },
                        new District{ Name="二林鎮" , ZipCode="526" },
                        new District{ Name="大城鄉" , ZipCode="527" },
                        new District{ Name="芳苑鄉" , ZipCode="528" },
                        new District{ Name="二水鄉" , ZipCode="530" },
                     
                }
            });

            allCity.Add(new City()
            {
                Code = "M",
                Name = "南投縣",
                TelCode = "049",
                Districts = new List<District>() 
                { 
                       new District{ Name="南投市" , ZipCode="540" },
                        new District{ Name="中寮鄉" , ZipCode="541" },
                        new District{ Name="草屯鎮" , ZipCode="542" },
                        new District{ Name="國姓鄉" , ZipCode="544" },
                        new District{ Name="埔里鎮" , ZipCode="545" },
                        new District{ Name="仁愛鄉" , ZipCode="546" },
                        new District{ Name="名間鄉" , ZipCode="551" },
                        new District{ Name="集集鎮" , ZipCode="552" },
                        new District{ Name="水里鄉" , ZipCode="553" },
                        new District{ Name="魚池鄉" , ZipCode="555" },
                        new District{ Name="信義鄉" , ZipCode="556" },
                        new District{ Name="竹山鎮" , ZipCode="557" },
                        new District{ Name="鹿谷鄉" , ZipCode="558" },
                     
                }
            });

            allCity.Add(new City()
            {
                Code = "Q",
                Name = "嘉義縣",
                TelCode = "05",
                Districts = new List<District>() 
                { 
                         new District{ Name="嘉義市" , ZipCode="600" },
                        new District{ Name="番路鄉" , ZipCode="602" },
                        new District{ Name="梅山鄉" , ZipCode="603" },
                        new District{ Name="竹崎鄉" , ZipCode="604" },
                        new District{ Name="阿里山鄉" , ZipCode="605" },
                        new District{ Name="中埔鄉" , ZipCode="606" },
                        new District{ Name="大埔鄉" , ZipCode="607" },
                        new District{ Name="水上鄉" , ZipCode="608" },
                        new District{ Name="鹿草鄉" , ZipCode="611" },
                        new District{ Name="太保市" , ZipCode="612" },
                        new District{ Name="朴子市" , ZipCode="613" },
                        new District{ Name="東石鄉" , ZipCode="614" },
                        new District{ Name="六腳鄉" , ZipCode="615" },
                        new District{ Name="新港鄉" , ZipCode="616" },
                        new District{ Name="民雄鄉" , ZipCode="621" },
                        new District{ Name="大林鎮" , ZipCode="622" },
                        new District{ Name="溪口鄉" , ZipCode="623" },
                        new District{ Name="義竹鄉" , ZipCode="624" },
                        new District{ Name="布袋鎮" , ZipCode="625" },
                     
                }
            });

            allCity.Add(new City()
            {
                Code = "P",
                Name = "雲林縣",
                TelCode = "05",
                Districts = new List<District>() 
                { 
                         new District{ Name="斗南鎮" , ZipCode="630" },
                        new District{ Name="大埤鄉" , ZipCode="631" },
                        new District{ Name="虎尾鎮" , ZipCode="632" },
                        new District{ Name="土庫鎮" , ZipCode="633" },
                        new District{ Name="褒忠鄉" , ZipCode="634" },
                        new District{ Name="東勢鄉" , ZipCode="635" },
                        new District{ Name="臺西鄉" , ZipCode="636" },
                        new District{ Name="崙背鄉" , ZipCode="637" },
                        new District{ Name="麥寮鄉" , ZipCode="638" },
                        new District{ Name="斗六市" , ZipCode="640" },
                        new District{ Name="林內鄉" , ZipCode="643" },
                        new District{ Name="古坑鄉" , ZipCode="646" },
                        new District{ Name="莿桐鄉" , ZipCode="647" },
                        new District{ Name="西螺鎮" , ZipCode="648" },
                        new District{ Name="二崙鄉" , ZipCode="649" },
                        new District{ Name="北港鎮" , ZipCode="651" },
                        new District{ Name="水林鄉" , ZipCode="652" },
                        new District{ Name="口湖鄉" , ZipCode="653" },
                        new District{ Name="四湖鄉" , ZipCode="654" },
                        new District{ Name="元長鄉" , ZipCode="655" },
                     
                }
            });

            allCity.Add(new City()
            {
                Code = "D",
                Name = "臺南市",
                TelCode = "06",
                Districts = new List<District>() 
                { 
                         new District{ Name="中西區" , ZipCode="700" },
                        new District{ Name="東區" , ZipCode="701" },
                        new District{ Name="南區" , ZipCode="702" },
                        new District{ Name="北區" , ZipCode="704" },
                        new District{ Name="安平區" , ZipCode="708" },
                        new District{ Name="安南區" , ZipCode="709" },
                        new District{ Name="永康區" , ZipCode="710" },
                        new District{ Name="歸仁區" , ZipCode="711" },
                        new District{ Name="新化區" , ZipCode="712" },
                        new District{ Name="左鎮區" , ZipCode="713" },
                        new District{ Name="玉井區" , ZipCode="714" },
                        new District{ Name="楠西區" , ZipCode="715" },
                        new District{ Name="南化區" , ZipCode="716" },
                        new District{ Name="仁德區" , ZipCode="717" },
                        new District{ Name="關廟區" , ZipCode="718" },
                        new District{ Name="龍崎區" , ZipCode="719" },
                        new District{ Name="官田區" , ZipCode="720" },
                        new District{ Name="麻豆區" , ZipCode="721" },
                        new District{ Name="佳里區" , ZipCode="722" },
                        new District{ Name="西港區" , ZipCode="723" },
                        new District{ Name="七 股區" , ZipCode="724" },
                        new District{ Name="將軍區" , ZipCode="725" },
                        new District{ Name="學甲區" , ZipCode="726" },
                        new District{ Name="北門區" , ZipCode="727" },
                        new District{ Name="新營區" , ZipCode="730" },
                        new District{ Name="後壁區" , ZipCode="731" },
                        new District{ Name="白河區" , ZipCode="732" },
                        new District{ Name="東山區" , ZipCode="733" },
                        new District{ Name="六甲區" , ZipCode="734" },
                        new District{ Name="下營區" , ZipCode="735" },
                        new District{ Name="柳營區" , ZipCode="736" },
                        new District{ Name="鹽水區" , ZipCode="737" },
                        new District{ Name="善化區" , ZipCode="741" },
                        new District{ Name="大內區" , ZipCode="742" },
                        new District{ Name="山上區" , ZipCode="743" },
                        new District{ Name="新市區" , ZipCode="744" },
                        new District{ Name="安定區" , ZipCode="745" },
                     
                }
            });

            allCity.Add(new City()
            {
                Code = "E",
                Name = "高雄市",
                TelCode = "07",
                Districts = new List<District>() 
                { 
                         new District{ Name="新興區" , ZipCode="800" },
                        new District{ Name="前金區" , ZipCode="801" },
                        new District{ Name="苓雅區" , ZipCode="802" },
                        new District{ Name="鹽埕區" , ZipCode="803" },
                        new District{ Name="鼓山區" , ZipCode="804" },
                        new District{ Name="旗津區" , ZipCode="805" },
                        new District{ Name="前鎮區" , ZipCode="806" },
                        new District{ Name="三民區" , ZipCode="807" },
                        new District{ Name="楠梓區" , ZipCode="811" },
                        new District{ Name="小港區" , ZipCode="812" },
                        new District{ Name="左營區" , ZipCode="813" },
                        new District{ Name="仁武區" , ZipCode="814" },
                        new District{ Name="大社區" , ZipCode="815" },
                        new District{ Name="岡山區" , ZipCode="820" },
                        new District{ Name="路竹區" , ZipCode="821" },
                        new District{ Name="阿蓮區" , ZipCode="822" },
                        new District{ Name="田寮區" , ZipCode="823" },
                        new District{ Name="燕巢區" , ZipCode="824" },
                        new District{ Name="橋頭區" , ZipCode="825" },
                        new District{ Name="梓官區" , ZipCode="826" },
                        new District{ Name="彌陀區" , ZipCode="827" },
                        new District{ Name="永安區" , ZipCode="828" },
                        new District{ Name="湖內區" , ZipCode="829" },
                        new District{ Name="鳳山區" , ZipCode="830" },
                        new District{ Name="大寮區" , ZipCode="831" },
                        new District{ Name="林園區" , ZipCode="832" },
                        new District{ Name="鳥松區" , ZipCode="833" },
                        new District{ Name="大樹區" , ZipCode="840" },
                        new District{ Name="旗山區" , ZipCode="842" },
                        new District{ Name="美濃區" , ZipCode="843" },
                        new District{ Name="六龜區" , ZipCode="844" },
                        new District{ Name="內門區" , ZipCode="845" },
                        new District{ Name="杉林區" , ZipCode="846" },
                        new District{ Name="甲仙區" , ZipCode="847" },
                        new District{ Name="桃源區" , ZipCode="848" },
                        new District{ Name="那瑪夏區" , ZipCode="849" },
                        new District{ Name="茂林區" , ZipCode="851" },
                        new District{ Name="茄萣區" , ZipCode="852" },
                     
                }
            });

            allCity.Add(new City()
            {
                Code = "X",
                Name = "澎湖縣",
                TelCode = "06",
                Districts = new List<District>() 
                { 
                       new District{ Name="馬公市" , ZipCode="880" },
                        new District{ Name="西嶼鄉" , ZipCode="881" },
                        new District{ Name="望安鄉" , ZipCode="882" },
                        new District{ Name="七美鄉" , ZipCode="883" },
                        new District{ Name="白沙鄉" , ZipCode="884" },
                        new District{ Name="湖西鄉" , ZipCode="885" },
                     
                }
            });


            allCity.Add(new City()
            {
                Code = "T",
                Name = "屏東縣",
                TelCode = "08",
                Districts = new List<District>() 
                { 
                       new District{ Name="屏東市" , ZipCode="900" },
                        new District{ Name="三地門鄉" , ZipCode="901" },
                        new District{ Name="霧臺鄉" , ZipCode="902" },
                        new District{ Name="瑪家鄉" , ZipCode="903" },
                        new District{ Name="九如鄉" , ZipCode="904" },
                        new District{ Name="里港鄉" , ZipCode="905" },
                        new District{ Name="高樹鄉" , ZipCode="906" },
                        new District{ Name="鹽埔鄉" , ZipCode="907" },
                        new District{ Name="長治鄉" , ZipCode="908" },
                        new District{ Name="麟洛鄉" , ZipCode="909" },
                        new District{ Name="竹田鄉" , ZipCode="911" },
                        new District{ Name="內埔鄉" , ZipCode="912" },
                        new District{ Name="萬丹鄉" , ZipCode="913" },
                        new District{ Name="潮州鎮" , ZipCode="920" },
                        new District{ Name="泰武鄉" , ZipCode="921" },
                        new District{ Name="來義鄉" , ZipCode="922" },
                        new District{ Name="萬巒鄉" , ZipCode="923" },
                        new District{ Name="崁頂鄉" , ZipCode="924" },
                        new District{ Name="新埤鄉" , ZipCode="925" },
                        new District{ Name="南州鄉" , ZipCode="926" },
                        new District{ Name="林邊鄉" , ZipCode="927" },
                        new District{ Name="東港鎮" , ZipCode="928" },
                        new District{ Name="琉球鄉" , ZipCode="929" },
                        new District{ Name="佳冬鄉" , ZipCode="931" },
                        new District{ Name="新園鄉" , ZipCode="932" },
                        new District{ Name="枋寮鄉" , ZipCode="940" },
                        new District{ Name="枋山鄉" , ZipCode="941" },
                        new District{ Name="春日鄉" , ZipCode="942" },
                        new District{ Name="獅子鄉" , ZipCode="943" },
                        new District{ Name="車城鄉" , ZipCode="944" },
                        new District{ Name="牡丹鄉" , ZipCode="945" },
                        new District{ Name="恆春鎮" , ZipCode="946" },
                        new District{ Name="滿州鄉" , ZipCode="947" },
                     
                }
            });

            allCity.Add(new City()
            {
                Code = "V",
                Name = "臺東縣",
                TelCode = "089",
                Districts = new List<District>() 
                { 
                       new District{ Name="臺東市" , ZipCode="950" },
                        new District{ Name="綠島鄉" , ZipCode="951" },
                        new District{ Name="蘭嶼鄉" , ZipCode="952" },
                        new District{ Name="延平鄉" , ZipCode="953" },
                        new District{ Name="卑南鄉" , ZipCode="954" },
                        new District{ Name="鹿野鄉" , ZipCode="955" },
                        new District{ Name="關山鎮" , ZipCode="956" },
                        new District{ Name="海端鄉" , ZipCode="957" },
                        new District{ Name="池上鄉" , ZipCode="958" },
                        new District{ Name="東河鄉" , ZipCode="959" },
                        new District{ Name="成功鎮" , ZipCode="961" },
                        new District{ Name="長濱鄉" , ZipCode="962" },
                        new District{ Name="太麻里鄉" , ZipCode="963" },
                        new District{ Name="金峰鄉" , ZipCode="964" },
                        new District{ Name="大武鄉" , ZipCode="965" },
                        new District{ Name="達仁鄉" , ZipCode="966" },
                     
                }
            });

            allCity.Add(new City()
            {
                Code = "U",
                Name = "花蓮縣",
                TelCode = "03",
                Districts = new List<District>() 
                { 
                     new District{ Name="花蓮市" , ZipCode="970" },
                        new District{ Name="新城鄉" , ZipCode="971" },
                        new District{ Name="秀林鄉" , ZipCode="972" },
                        new District{ Name="吉安鄉" , ZipCode="973" },
                        new District{ Name="壽豐鄉" , ZipCode="974" },
                        new District{ Name="鳳林鎮" , ZipCode="975" },
                        new District{ Name="光復鄉" , ZipCode="976" },
                        new District{ Name="豐濱鄉" , ZipCode="977" },
                        new District{ Name="瑞穗鄉" , ZipCode="978" },
                        new District{ Name="萬榮鄉" , ZipCode="979" },
                        new District{ Name="玉里鎮" , ZipCode="981" },
                        new District{ Name="卓溪鄉" , ZipCode="982" },
                        new District{ Name="富里鄉" , ZipCode="983" },
                     
                }
            });

            allCity.Add(new City()
            {
                Code = "W",
                Name = "金門縣",
                TelCode = "082",
                Districts = new List<District>() 
                { 
                     new District{ Name="金沙鎮" , ZipCode="890" },
                        new District{ Name="金湖鎮" , ZipCode="891" },
                        new District{ Name="金寧鄉" , ZipCode="892" },
                        new District{ Name="金城鎮" , ZipCode="893" },
                        new District{ Name="烈嶼鄉" , ZipCode="894" },
                        new District{ Name="烏坵鄉" , ZipCode="896" },
                     
                }
            });

            allCity.Add(new City()
            {
                Code = "Z",
                Name = "連江縣",
                TelCode = "0836",
                Districts = new List<District>() 
                { 
                       new District{ Name="南   竿" , ZipCode="209" },
                        new District{ Name="北   竿" , ZipCode="210" },
                        new District{ Name="莒   光" , ZipCode="211" },
                        new District{ Name="東   引" , ZipCode="212" },
                     
                }
            });


            foreach (City c in allCity)
            {
                context.Cities.Add(c);
            }

        }

        public static void CreateRoles(AppRoleManager roleManager)
        {
            roleManager.Create(new AppRole()
            {
                Name = "Boss",
                Description = "老闆",
                IsAdmin = true,
                Order = 1,
            });

            roleManager.Create(new AppRole()
            {
                Name = "Admin",
                Description = "管理員",
                IsAdmin = true,
                Order = 2
            });
            roleManager.Create(new AppRole()
            {
                Name = "Member",
                Description = "會員",
                IsAdmin = false,
                Order = 3
            });

        }
        public static void CreateBoss(AppUserManager userManager, int city, int district)
        {
            var user = new AppUser()
            {
                Email = "opmart2008@yahoo.com.tw",
                PhoneNumber = "0936060049",
                DOB = new DateTime(1979, 3, 12),
                LastName = "何",
                FirstName = "金水",
                Gender = true,
                UserName = "opmart2008@yahoo.com.tw",
                Address = new Address { CityId = city, DistrictId = district, StreetAddress = "花蓮縣吉安鄉光華八街199號", ZipCode = "973" },
                CreateDate = DateTime.Now,
                EmailConfirmed = true
            };
            string password = "Jeremy07";
            var result = userManager.Create(user, password);
            if (result.Succeeded) userManager.AddToRole(user.Id, "Boss");



        }
    }

    public class SeedDataCreator
    {
        InmallContext context;
        public string DataFolder { get; set; }

        public SeedDataCreator(InmallContext context)
        {
            this.context = context;
        }
        public SeedDataCreator(InmallContext context, string dataFolder)
        {
            this.context = context;
            this.DataFolder = dataFolder;
        }
        public void CreateProducts(XmlDocument xmlDocument)
        {
            string rootName = "Products";
            XmlNode root = xmlDocument.DocumentElement;
            if (root.Name != rootName)
            {
                throw new Exception("wrong root Name. root name should be: " + rootName);
            }

            foreach (XmlNode node in root.ChildNodes)
            {
                AddProducts(node);
            }

            context.SaveChanges();
        }

        void AddProducts(XmlNode node)
        {
            var nodeName = node.SelectSingleNode("Name");
            string name = nodeName.InnerText;

            var exsit = context.Products.Where(p => p.Name == name).FirstOrDefault();
            if (exsit != null) return;

            var category = FIndCategory(node);
            var kind = GetKinds(node);

            var nodeOnlineId = node.SelectSingleNode("OnlineId");

            var nodePrice = node.SelectSingleNode("Price");
            int price = nodePrice.InnerText.ToInt();
            var nodeDescription = node.SelectSingleNode("Description");
            var nodeWaitDays = node.SelectSingleNode("WaitDays");
            int waitDays = nodeWaitDays.InnerText.ToInt();

            var summary = GetSummary(node);
            var features = GetFeatures(node);

            var photoList = GetProductPhotoList(node);
            var description = GetDescription(node);

            var specification = GetSpecification(node);
            var supplier = FIndSupplier(node);

            var tags = GetTags(node);



            var product = new Product
            {
                OnlineId = nodeOnlineId.InnerText.ToInt(),
                Name = name,
                Price = price,
                Kind = kind,
                Categories = new List<Category>(),
                Active = true,
                CreateDate = DateTime.Now,
                Summary = summary,
                Features = features,
                Description = description,
                Specification = specification,
                WaitDays = waitDays,
                Photoes = photoList
            };


            product.Categories.Add(category);
            product.Categories.Add(supplier);

            context.Products.Add(product);
        }
        ProductCategory FIndCategory(XmlNode node)
        {
            var nodeCategory = node.SelectSingleNode("Category");
            string name = nodeCategory.InnerText;
            ProductCategory category = context.ProductCategorys.Where(s => s.Name == name).FirstOrDefault();

            if (category != null) return category;

            throw new Exception("No Category: Found: " + name);
        }

        Supplier FIndSupplier(XmlNode node)
        {
            var nodeSupplier = node.SelectSingleNode("Supplier");
            string name = nodeSupplier.SelectSingleNode("Name").InnerText.Trim();
            Supplier supplier = context.Suppliers.Where(s => s.Name == name).FirstOrDefault();

            if (supplier != null) return supplier;


            string contact = nodeSupplier.SelectSingleNode("Contact").InnerText;
            string Address = nodeSupplier.SelectSingleNode("Address").InnerText;
            string tel = nodeSupplier.SelectSingleNode("Tel").InnerText;
            string Phone = nodeSupplier.SelectSingleNode("Phone").InnerText;
            string email = nodeSupplier.SelectSingleNode("Email").InnerText;

            var description = GetDescription(nodeSupplier);

            supplier = new Supplier
            {
                Active = true,
                Contact = contact,
                Tel = tel,
                Phone = Phone,
                Email = email,
                Name = name,
                Description = description

            };

            context.Suppliers.Add(supplier);


            return supplier;
        }

        ProductSummary GetSummary(XmlNode node)
        {
            var nodeSummary = node.SelectSingleNode("Summary");

            var itemNodeList = nodeSummary.ChildNodes;

            var summary = new ProductSummary();

            XmlNode itemNode = itemNodeList[0];
            if (itemNode != null) summary.Item1 = itemNode.InnerText;

            itemNode = itemNodeList[1];
            if (itemNode != null) summary.Item2 = itemNode.InnerText;

            itemNode = itemNodeList[2];
            if (itemNode != null) summary.Item3 = itemNode.InnerText;

            itemNode = itemNodeList[3];
            if (itemNode != null) summary.Item4 = itemNode.InnerText;

            itemNode = itemNodeList[4];
            if (itemNode != null) summary.Item5 = itemNode.InnerText;

            return summary;
        }

        ProductFeatures GetFeatures(XmlNode node)
        {
            var nodeFeatures = node.SelectSingleNode("Features");

            if (!nodeFeatures.HasChildNodes) return new ProductFeatures();


            var itemNodeList = nodeFeatures.ChildNodes;

            var features = new ProductFeatures();

            XmlNode itemNode = itemNodeList[0];
            if (itemNode != null) features.Item1 = itemNode.InnerText;

            itemNode = itemNodeList[1];
            if (itemNode != null) features.Item2 = itemNode.InnerText;

            itemNode = itemNodeList[2];
            if (itemNode != null) features.Item3 = itemNode.InnerText;

            itemNode = itemNodeList[3];
            if (itemNode != null) features.Item4 = itemNode.InnerText;

            itemNode = itemNodeList[4];
            if (itemNode != null) features.Item5 = itemNode.InnerText;

            return features;
        }

        ProductSpecification GetSpecification(XmlNode node)
        {
            var nodeSpecification = node.SelectSingleNode("Specification");

            if (!nodeSpecification.HasChildNodes) return new ProductSpecification();


            string madeBy = nodeSpecification.SelectSingleNode("MadeBy").InnerText;
            string size = nodeSpecification.SelectSingleNode("Size").InnerText;
            string madeIn = nodeSpecification.SelectSingleNode("MadeIn").InnerText;
            string ps = nodeSpecification.SelectSingleNode("PS").InnerText;

            var specification = new ProductSpecification
            {

                MadeBy = madeBy,
                Size = size,
                MadeIn = madeIn,
                PS = ps

            };

            return specification;
        }
        Description GetDescription(XmlNode node)
        {
            var nodeDescription = node.SelectSingleNode("Description");

            if (!nodeDescription.HasChildNodes) return new Description();


            string title = nodeDescription.SelectSingleNode("Title").InnerText;
            string content = nodeDescription.SelectSingleNode("Content").InnerText;

            var description = new Description
            {
                Title = title,
                Content = content,
            };

            return description;
        }

        List<ProductPhoto> GetProductPhotoList(XmlNode node)
        {
            var photoList = new List<ProductPhoto>();
            var nodePhotoes = node.SelectSingleNode("Photoes");

            string folderPath = GetFolderPath(nodePhotoes);

            foreach (XmlNode itemNode in nodePhotoes.ChildNodes)
            {
                photoList.Add(GetProductPhoto(itemNode, folderPath));
            }

            return photoList;

        }

        List<Tag> GetTags(XmlNode node)
        {
            var tagList = new List<Tag>();
            var nodeTags = node.SelectSingleNode("Tags");

            foreach (XmlNode itemNode in nodeTags.ChildNodes)
            {
                var tagName = itemNode.InnerText;
                Tag tag = context.Tags.Where(t => t.Name.Trim() == tagName).FirstOrDefault();
                if (tag == null)
                {
                    tag = new Tag { Name = tagName };
                    context.Tags.Add(tag);
                    context.SaveChanges();
                }
                tagList.Add(tag);
            }

            return tagList;
        }
        string GetKinds(XmlNode node)
        {
            var nodeKinds = node.SelectSingleNode("Kind");
            if (nodeKinds == null) return "";
            return nodeKinds.InnerText;
        }
        public void CreateParentCategories(XmlDocument xmlDocument)
        {
            string rootName = "Categories";
            XmlNode root = xmlDocument.DocumentElement;

            if (root.Name != rootName)
            {
                throw new Exception("wrong root Name. root name should be: " + rootName);
            }
            foreach (XmlNode node in root.ChildNodes)
            {
                AddCategory(node, 0);
            }
            context.SaveChanges();
        }
        public void CreateSubCategories(XmlDocument xmlDocument)
        {
            string rootName = "Categories";
            XmlNode root = xmlDocument.DocumentElement;

            if (root.Name != rootName)
            {
                throw new Exception("wrong root Name. root name should be: " + rootName);
            }

            foreach (XmlNode node in root.ChildNodes)
            {
                XmlElement element = (XmlElement)node;
                string parentName = element.GetAttributeNode("Name").Value;

                var parentCategory = context.Categories.Where(x => x.Name == parentName && x.Parent == 0).FirstOrDefault();
                if (parentCategory == null) { throw new Exception("parentCategory==null  name:" + parentName); }

                var childs = node.ChildNodes.Count > 0;
                if (childs)
                {
                    int parentId = parentCategory.Id;
                    foreach (XmlNode item in node.ChildNodes)
                    {
                        AddCategory(item, parentId);
                    }

                }
            }

            context.SaveChanges();
        }

        void AddCategory(XmlNode node, int parentId)
        {

            Category category;

            XmlElement element = (XmlElement)node;
            string name = element.GetAttributeNode("Name").Value;
            string prefix = element.GetAttributeNode("Prefix").Value;
            int displayOrder = Convert.ToInt32(element.GetAttributeNode("DisplayOrder").Value);

            category = context.Categories.Where(x => x.Name == name && x.Parent == parentId).FirstOrDefault();
            if (category != null)
            {
                category.DisplayOrder = displayOrder;
                category.Active = true;
            }
            else
            {

                category = new ProductCategory()
                {
                    Name = name,
                    Prefix = prefix,
                    Parent = parentId,
                    DisplayOrder = displayOrder,
                    Active = true

                };
                context.Categories.Add(category);
            }

        }



        string GetFolderPath(XmlNode node)
        {
            XmlElement element = (XmlElement)node;
            string path = element.GetAttributeNode("Path").Value;
            string[] folders = path.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            string folderPath = @"C:\Users\Stephen\Pictures\Inmall";
            for (int i = 0; i < folders.Length; i++)
            {
                folderPath = Path.Combine(folderPath, folders[i]);
            }

            return folderPath;
        }

        ProductPhoto GetProductPhoto(XmlNode node, string folderPath)
        {
            XmlElement element = (XmlElement)node;
            string caption = element.GetAttributeNode("Caption").Value;
            string file = element.GetAttributeNode("File").Value;
            string top = element.GetAttributeNode("Top").Value;
            string displayOrder = element.GetAttributeNode("DisplayOrder").Value;

            ProductPhoto photo = new ProductPhoto
            {
                Caption = caption,
                Top = (top.ToInt() > 0),
                DisplayOrder = displayOrder.ToInt()
            };

            string fileName = Path.Combine(folderPath, file);
            var fileInfo = new FileInfo(fileName);
            photo.FileBytes = ImageHelper.GetByteFromImageFile(fileInfo);

            return photo;
        }

        void AddRatings(Product product)
        {
            int pid = product.Id;
            int star = 1;
            var random = new Random();
            int count = random.Next(1, 30);
            for (int i = 0; i < count; i++)
            {
                context.Ratings.Add(new Rating { ProductId = pid, Star = star });
            }
            count = random.Next(1, 30);
            star = 2;
            for (int i = 0; i < count; i++)
            {
                context.Ratings.Add(new Rating { ProductId = pid, Star = star });
            }
            count = random.Next(1, 30);
            star = 3;
            for (int i = 0; i < count; i++)
            {
                context.Ratings.Add(new Rating { ProductId = pid, Star = star });
            }
            count = random.Next(1, 30);
            star = 4;
            for (int i = 0; i < count; i++)
            {
                context.Ratings.Add(new Rating { ProductId = pid, Star = star });
            }
            count = random.Next(1, 30);
            star = 5;
            for (int i = 0; i < count; i++)
            {
                context.Ratings.Add(new Rating { ProductId = pid, Star = star });
            }

        }

        public void CreateEthnicGroups(XmlDocument xmlDocument)
        {
            string rootName = "EthnicGroups";
            XmlNode root = xmlDocument.DocumentElement;
            if (root.Name != rootName)
            {
                throw new Exception("wrong root Name. root name should be: " + rootName);
            }
            var parent = context.Categories.Where(e => e.Name == "原民族群").FirstOrDefault();
            if (parent == null) throw new Exception();
            int parentId=parent.Id;
            foreach (XmlNode node in root.ChildNodes)
            {
                AddEthnicGroups(node, parentId);
            }

            context.SaveChanges();
        }

        void AddEthnicGroups(XmlNode node ,int parent)
        {
            EthnicGroup ethnicGroup;

            XmlElement element = (XmlElement)node;
            string name = element.GetAttributeNode("Name").Value;
            string prefix = element.GetAttributeNode("Prefix").Value;
            int displayOrder = Convert.ToInt32(element.GetAttributeNode("DisplayOrder").Value);

            ethnicGroup = context.EthnicGroups.Where(e => e.Name.Trim() == name).FirstOrDefault();
            if (ethnicGroup != null)
            {
                ethnicGroup.Parent = parent;
                ethnicGroup.DisplayOrder = displayOrder;
                ethnicGroup.Active = true;
            }
            else
            {
                ethnicGroup = new EthnicGroup()
                {
                    Parent = parent,
                    DisplayOrder = displayOrder,
                    Name = name,
                    Active = true,
                    Products = new List<Product>(),

                };
            }

            if (ethnicGroup.Photoes.IsNullOrEmpty()) ethnicGroup.Photoes = new List<CategoryPhoto>();
            if (ethnicGroup.Products.IsNullOrEmpty()) ethnicGroup.Products = new List<Product>();

            var photo = new CategoryPhoto
            {
                Caption = name,
                Top = true,

            };


            string folderPath = @"C:\Users\Stephen\Pictures\Inmall\原民族群";
            string fileName = name + ".png";
            

            var fileInfo = new FileInfo(Path.Combine(folderPath, fileName));
            photo.FileBytes = ImageHelper.GetByteFromImageFile(fileInfo, "png");

            ethnicGroup.Photoes.Add(photo);

            if (node.HasChildNodes)
            {
                foreach (XmlNode itemNode in node.ChildNodes)
                {
                  string productName = itemNode.InnerText;
                  var product = context.Products.Where(p=>p.Name==productName).FirstOrDefault();
                  if (product != null)
                  {
                      ethnicGroup.Products.Add(product);
                  }
                   
                }
            }

            context.EthnicGroups.Add(ethnicGroup);

        }


        public void CreateTags(XmlDocument xmlDocument)
        {
            string rootName = "Products";
            XmlNode root = xmlDocument.DocumentElement;
            if (root.Name != rootName)
            {
                throw new Exception("wrong root Name. root name should be: " + rootName);
            }

            foreach (XmlNode node in root.ChildNodes)
            {
                 var nodeName = node.SelectSingleNode("Name");
                 string name = nodeName.InnerText;
                 var product=context.Products.Where(p=>p.Name.Trim()==name).FirstOrDefault();
                 var tags = GetTags(node);

                 product.Tags = tags;
            }

            context.SaveChanges();
        }

      


    }//end class
}
