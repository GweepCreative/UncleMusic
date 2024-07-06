using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PuppeteerSharp;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;
using NPOI.XWPF.UserModel;

namespace TurkulerimizDownloader
{
    public partial class Form1 : Form
    {

        private CancellationTokenSource cancellationTokenSource;
        private List<string> downloadedFiles = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeMusicList()
        {
            var urlsFilePath = "urls.txt";

            try
            {
                var lines = File.ReadAllLines(urlsFilePath);
                foreach (var url in lines)
                {
                    musicList.Items.Add(url.Replace("/turkuler/", "").Replace("/", "").Replace("-", " "));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load URLs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnFetchData.Enabled = btnStop.Enabled = btnOpenDir.Enabled = true;
                status.Visible = false;
                label1.Text += $": {musicList.Items.Count}";
            }
        }
        private async System.Threading.Tasks.Task FetchDataAsync(CancellationToken cancellationToken)
        {
            btnFetchData.Enabled = false;
            var urlsFilePath = "urls.txt";

            try
            {
                await new BrowserFetcher().DownloadAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to download Chromium: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cancellationTokenSource = null;
                btnFetchData.Enabled = true;
                return;
            }

            var options = new LaunchOptions
            {
                Headless = true
            };

            using (var browser = await Puppeteer.LaunchAsync(options))
            using (var page = await browser.NewPageAsync())
            {
                var lines = File.ReadAllLines(urlsFilePath);
                var totalUrls = lines.Length;
                var currentProgress = 0;

                foreach (var url in lines)
                {
                    if (cancellationToken.IsCancellationRequested)
                        break;

                    await page.GoToAsync("https://turkulerimiz.biz" + url);

                    var data = await page.EvaluateFunctionAsync(@"() => {
                        const result = [];
                        const cardBody = document.querySelectorAll('.card-body')[1];

                        if (cardBody) {
                            let textContent = cardBody.innerHTML.replace(/\n/g, '').split('<!')[0].split('<br>');

                            textContent = textContent.filter(Boolean).map(line => line.trim()).join('\n');

                            const images = Array.from(cardBody.querySelectorAll('img')).map(img => img.getAttribute('src'));

                            result.push({
                                textContent: textContent,
                                imageSources: images
                            });
                        }

                        return result;
                    }");

                    // Verileri dosyaya kaydet
                    var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    var fileName = $"{Uri.EscapeDataString(url.Replace("/turkuler/", "").Replace("/", ""))}";
                    var filePath = Path.Combine(directory, fileName);

                    var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);


                    var textContent = data[0]["textContent"].ToString();
                    var imageCotent = data[0]["imageSources"].HasValues ? "https://turkulerimiz.biz" + data[0]["imageSources"][0].ToString() : null;

                    createWordFile(textContent, imageCotent, filePath, fileName);


                    downloadedFiles.Add(fileName);
                    complatedList.Items.Add(fileName);

                    // Ýlerleme çubuðunu ve etiketi güncelle
                    currentProgress = (int)(((double)downloadedFiles.Count / totalUrls) * 100);
                    progressBar1.Value = currentProgress;
                    lblProgress.Text = $"{currentProgress}% | {downloadedFiles.Count}/{totalUrls}";

                    // Müzik adýný güncelle
                    downloadingNow.Text = "Þuanda indiriliyor: \n" + fileName;

                    // Kýsa bir bekleme süresi ekleyebilirsiniz (opsiyonel)
                    await System.Threading.Tasks.Task.Delay(500);
                }

                MessageBox.Show("Download completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            cancellationTokenSource = null;
            btnFetchData.Enabled = true;
        }

        private async void createWordFile(string content, string image, string path, string fileName)
        {
            using (XWPFDocument doc = new XWPFDocument())
            {
                XWPFParagraph p1 = doc.CreateParagraph();
                p1.Alignment = ParagraphAlignment.CENTER;
                p1.VerticalAlignment = TextAlignment.TOP;
                XWPFRun r1 = p1.CreateRun();
                var sozlerList = content.Split("\n");
                foreach(var satir in sozlerList)
                {
                    r1.AppendText(satir);
                    r1.AddBreak(BreakType.TEXTWRAPPING);
                }
                if (!String.IsNullOrEmpty(image))
                {
                    using(HttpClient client = new HttpClient())
                    {
                        try
                        {
                            using(Stream stream = await client.GetStreamAsync(image))
                            {
                                r1.AddPicture(stream, (int)PictureType.PNG, "image1", (int)(400.0 * 9525), (int)(500.0 * 9525));
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Hata oluþtu: {ex.Message}");
                        }
                    }
                }
                using (var fs = new FileStream($"{path}.docx", FileMode.Create))
                {
                    doc.Write(fs);
                }
            }
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource = null;
            }
        }

        private async void btnFetchData_Click(object sender, EventArgs e)
        {
            if (cancellationTokenSource != null)
            {
                MessageBox.Show("Operation already in progress.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            cancellationTokenSource = new CancellationTokenSource();
            await FetchDataAsync(cancellationTokenSource.Token);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeMusicList();
        }

        private void btnOpenDir_Click(object sender, EventArgs e)
        {
            
        }
    }
}
