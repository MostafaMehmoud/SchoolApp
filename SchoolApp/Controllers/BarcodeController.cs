using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;
using SkiaSharp;

public class BarcodeController : Controller
{
    // 🆕 توليد الباركود من رقم الطالب وإرجاعه كصورة
    public IActionResult GenerateBarcode(string studentNumber)
    {
        if (string.IsNullOrEmpty(studentNumber))
        {
            return BadRequest("❌ رقم الطالب غير صحيح!");
        }

        try
        {
            // ✅ إنشاء كائن توليد الباركود
            var barcodeWriter = new BarcodeWriterSvg
            {
                Format = BarcodeFormat.CODE_128, // ✅ نوع الباركود
                Options = new EncodingOptions
                {
                    Width = 300,  // ✅ العرض
                    Height = 100, // ✅ الارتفاع
                    Margin = 10   // ✅ هامش
                }
            };

            // ✅ توليد صورة الباركود
            var barcodeSvg = barcodeWriter.Write(studentNumber);

            // ✅ تحويل SVG إلى `MemoryStream`
            using MemoryStream memoryStream = new MemoryStream();
            using StreamWriter writer = new StreamWriter(memoryStream);
            writer.Write(barcodeSvg.Content);
            writer.Flush();
            memoryStream.Position = 0;

            return File(memoryStream.ToArray(), "image/svg+xml");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"❌ خطأ في توليد الباركود: {ex.Message}");
        }
    }
}
