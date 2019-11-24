using PaJaMa.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace PaJaMa.WinControls
{
	public static class ImagingExtensions
	{
		public static Image CropImage(this Image image, Rectangle area)
		{
			Bitmap bmpImage = new Bitmap(image);
			return (Image)bmpImage.Clone(area, bmpImage.PixelFormat);
		}

		public static Bitmap CropImage(this Bitmap image, Rectangle area)
		{
			Bitmap bmpImage = new Bitmap(image);
			return bmpImage.Clone(area, bmpImage.PixelFormat);
		}
	}

	public class Imaging
	{
		#region Crop
		public static Image CropImage(string inputFile, Rectangle area)
		{
			return Image.FromFile(inputFile).CropImage(area);
		}

		public static Image CropImage(Stream stream, Rectangle area)
		{
			return Image.FromStream(stream).CropImage(area);
		}

		public static void CropImageToFile(Image image, Rectangle area, string outputFile)
		{
			image.CropImage(area).Save(outputFile);
		}

		public static void CropImageToFile(Image image, Rectangle area, string outputFile, ImageFormat format)
		{
			image.CropImage(area).Save(outputFile, format);
		}

		public static void CropImageToFile(string inputFile, Rectangle area, string outputFile)
		{
			CropImage(inputFile, area).Save(outputFile);
		}

		public static void CropImageToFile(string inputFile, Rectangle area, string outputFile, ImageFormat format)
		{
			CropImage(inputFile, area).Save(outputFile, format);
		}

		public static void CropImageToFile(Stream stream, Rectangle area, string outputFile)
		{
			CropImage(stream, area).Save(outputFile);
		}

		public static void CropImageToFile(Stream stream, Rectangle area, string outputFile, ImageFormat format)
		{
			CropImage(stream, area).Save(outputFile, format);
		}
		#endregion

		#region Resize
		public static Image ResizeImage(Image image, Size size, bool stretch = false)
		{
			Bitmap b = new Bitmap(size.Width, size.Height);
			using (var g = Graphics.FromImage((Image)b))
			{
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;

				int drawWidth = size.Width;
				int drawHeight = size.Height;
				int x = 0;
				int y = 0;
				if (!stretch)
				{
					double ratio = (double)image.Width / (double)image.Height;
					if (image.Width > image.Height)
						drawHeight = (int)(drawWidth / ratio);
					else
						drawWidth = (int)(drawHeight * ratio);

					x = (size.Width - drawWidth) / 2;
					y = (size.Height - drawHeight) / 2;
				}
				g.DrawImage(image, x, y, drawWidth, drawHeight);
			}

			return (Image)b;
		}

		public static Image ResizeImage(string inputFile, Size size, bool stretch = false)
		{
			return ResizeImage(Image.FromFile(inputFile), size, stretch);
		}

		public static Image ResizeImage(Stream stream, Size size, bool stretch = false)
		{
			return ResizeImage(Image.FromStream(stream), size, stretch);
		}

		public static void ResizeImageToFile(Image image, Size size, string outputFile, bool stretch = false)
		{
			ResizeImage(image, size, stretch).Save(outputFile);
		}

		public static void ResizeImageToFile(Image image, Size size, string outputFile, ImageFormat format, bool stretch = false)
		{
			ResizeImage(image, size, stretch).Save(outputFile, format);
		}

		public static void ResizeImageToFile(string inputFile, Size size, string outputFile, bool stretch = false)
		{
			ResizeImage(inputFile, size, stretch).Save(outputFile);
		}

		public static void ResizeImageToFile(string inputFile, Size size, string outputFile, ImageFormat format, bool stretch = false)
		{
			ResizeImage(inputFile, size, stretch).Save(outputFile, format);
		}

		public static void ResizeImageToFile(Stream stream, Size size, string outputFile, bool stretch = false)
		{
			ResizeImage(stream, size, stretch).Save(outputFile);
		}

		public static void ResizeImageToFile(Stream stream, Size size, string outputFile, ImageFormat format, bool stretch = false)
		{
			ResizeImage(stream, size, stretch).Save(outputFile, format);
		}
		#endregion

		#region ConvertImageToIcon
		public static void ConvertImageToIcon(Image image, string outputFile)
		{
			Image img32 = ResizeImage(image, new Size(32, 32));
			Image img16 = ResizeImage(image, new Size(16, 16));
			FileInfo inf32 = new FileInfo(Path.GetTempFileName() + ".png");
			FileInfo inf16 = new FileInfo(Path.GetTempFileName() + ".png");
			FileInfo png2icoInf = new FileInfo(Path.GetTempPath() + "png2ico.exe");
			img32.Save(inf32.FullName, ImageFormat.Png);
			img16.Save(inf16.FullName, ImageFormat.Png);
			File.WriteAllBytes(png2icoInf.FullName, Resources.png2ico);
			string output = PaJaMa.Common.Common.ExecuteFile(png2icoInf.FullName, "\"" + outputFile + "\" \""
				+ inf16.FullName + "\" \"" + inf32.FullName + "\"");
			inf32.Delete();
			inf16.Delete();
			png2icoInf.Delete();
		}

		public static void ConvertImageToIcon(string inputFile, string outputFile)
		{
			ConvertImageToIcon(Image.FromFile(inputFile), outputFile);
		}

		#endregion

		#region GetIconForFile
		public static Icon GetIconForFile(string file, bool large)
		{
			SHFILEINFO shinfo = new SHFILEINFO();
			IntPtr hImg = Win32.SHGetFileInfo(file, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo),
				Win32.SHGFI_ICON | (large ? Win32.SHGFI_LARGEICON : Win32.SHGFI_SMALLICON));
			Icon ico = Icon.FromHandle(shinfo.hIcon);
			Win32.DestroyIcon(hImg);
			return ico;
		}

		public static Image GetImageForFile(string file, bool large)
		{
			return GetIconForFile(file, large).ToBitmap();
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SHFILEINFO
		{
			public IntPtr hIcon;
			public IntPtr iIcon;
			public uint dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szTypeName;
		};

		public class Win32
		{
			public const uint SHGFI_ICON = 0x100;
			public const uint SHGFI_LARGEICON = 0x0;    // 'Large icon
			public const uint SHGFI_SMALLICON = 0x1;    // 'Small icon

			[DllImport("shell32.dll")]
			public static extern IntPtr SHGetFileInfo(string pszPath,
										uint dwFileAttributes,
										ref SHFILEINFO psfi,
										uint cbSizeFileInfo,
										uint uFlags);

			[DllImport("user32.dll", SetLastError = true)]
			public static extern bool DestroyIcon(IntPtr hIcon);

		}
		#endregion

		#region BlackWhite
		public Image ImageToGrayscale(Image img)
		{
			Bitmap original = new Bitmap(img);

			//make an empty bitmap the same size as original
			Bitmap newBitmap = new Bitmap(original.Width, original.Height);

			int totProgress = 0;
			for (int i = 0; i < original.Width; i++)
			{
				for (int j = 0; j < original.Height; j++)
				{
					//get the pixel from the original image
					Color originalColor = original.GetPixel(i, j);

					//create the grayscale version of the pixel
					int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59)
						+ (originalColor.B * .11));

					//create the color object
					Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);

					//set the new image's pixel to the grayscale version
					newBitmap.SetPixel(i, j, newColor);

					totProgress++;
					if (ImageToGrayScaleProgress != null)
						ImageToGrayScaleProgress(this, new ProgressEventArgs((int)(100 * (decimal)totProgress / (decimal)(original.Width * original.Height))));
				}
			}

			return newBitmap;
		}

		public Image ImageToGrayscale(string imgFile)
		{
			return ImageToGrayscale(Image.FromFile(imgFile));
		}

		public void ImageToGrayscaleToFile(string inputFile, string outputFile)
		{
			ImageToGrayscale(inputFile).Save(outputFile);
		}

		public event ProgressEventHandler ImageToGrayScaleProgress;
		#endregion

		#region GetDimensions
		const string errorMessage = "Could not recognise image format.";

		private static Dictionary<byte[], Func<BinaryReader, Size>> imageFormatDecoders = new Dictionary<byte[], Func<BinaryReader, Size>>()
          { 
              { new byte[] { 0x42, 0x4D }, DecodeBitmap }, 
              { new byte[] { 0x47, 0x49, 0x46, 0x38, 0x37, 0x61 }, DecodeGif }, 
              { new byte[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 }, DecodeGif }, 
              { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }, DecodePng },
              { new byte[] { 0xff, 0xd8 }, DecodeJfif }, 
          };

		public static Size GetDimensions(BinaryReader binaryReader)
		{
			int maxMagicBytesLength = imageFormatDecoders.Keys.OrderByDescending(x => x.Length).First().Length;
			byte[] magicBytes = new byte[maxMagicBytesLength];
			for (int i = 0; i < maxMagicBytesLength; i += 1)
			{
				magicBytes[i] = binaryReader.ReadByte();
				foreach (var kvPair in imageFormatDecoders)
				{
					if (StartsWith(magicBytes, kvPair.Key))
					{
						return kvPair.Value(binaryReader);
					}
				}
			}

			throw new ArgumentException(errorMessage, "binaryReader");
		}

		public static Size GetDimensions(string path)
		{
			try
			{
				using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(path)))
				{
					try
					{
						return GetDimensions(binaryReader);
					}
					catch (ArgumentException e)
					{
						string newMessage = string.Format("{0} file: '{1}' ", errorMessage, path);

						throw new ArgumentException(newMessage, "path", e);
					}
				}
			}
			catch (ArgumentException)
			{
				//do it the old fashioned way

				using (Bitmap b = new Bitmap(path))
				{
					return b.Size;
				}
			}
		}

		private static bool StartsWith(byte[] thisBytes, byte[] thatBytes)
		{
			for (int i = 0; i < thatBytes.Length; i += 1)
			{
				if (thisBytes[i] != thatBytes[i])
				{
					return false;
				}
			}

			return true;
		}

		private static Size DecodeBitmap(BinaryReader binaryReader)
		{
			binaryReader.ReadBytes(16);
			int width = binaryReader.ReadInt32();
			int height = binaryReader.ReadInt32();
			return new Size(width, height);
		}

		private static Size DecodeGif(BinaryReader binaryReader)
		{
			int width = binaryReader.ReadInt16();
			int height = binaryReader.ReadInt16();
			return new Size(width, height);
		}

		private static Size DecodePng(BinaryReader binaryReader)
		{
			binaryReader.ReadBytes(8);
			int width = ReadLittleEndianInt32(binaryReader);
			int height = ReadLittleEndianInt32(binaryReader);
			return new Size(width, height);
		}

		private static Size DecodeJfif(BinaryReader binaryReader)
		{
			while (binaryReader.ReadByte() == 0xff)
			{
				byte marker = binaryReader.ReadByte();
				short chunkLength = ReadLittleEndianInt16(binaryReader);
				if (marker == 0xc0)
				{
					binaryReader.ReadByte();
					int height = ReadLittleEndianInt16(binaryReader);
					int width = ReadLittleEndianInt16(binaryReader);
					return new Size(width, height);
				}

				if (chunkLength < 0)
				{
					ushort uchunkLength = (ushort)chunkLength;
					binaryReader.ReadBytes(uchunkLength - 2);
				}
				else
				{
					binaryReader.ReadBytes(chunkLength - 2);
				}
			}

			throw new ArgumentException(errorMessage);
		}

		private static short ReadLittleEndianInt16(BinaryReader binaryReader)
		{
			byte[] bytes = new byte[sizeof(short)];

			for (int i = 0; i < sizeof(short); i += 1)
			{
				bytes[sizeof(short) - 1 - i] = binaryReader.ReadByte();
			}
			return BitConverter.ToInt16(bytes, 0);
		}

		private static ushort ReadLittleEndianUInt16(BinaryReader binaryReader)
		{
			byte[] bytes = new byte[sizeof(ushort)];

			for (int i = 0; i < sizeof(ushort); i += 1)
			{
				bytes[sizeof(ushort) - 1 - i] = binaryReader.ReadByte();
			}
			return BitConverter.ToUInt16(bytes, 0);
		}

		private static int ReadLittleEndianInt32(BinaryReader binaryReader)
		{
			byte[] bytes = new byte[sizeof(int)];
			for (int i = 0; i < sizeof(int); i += 1)
			{
				bytes[sizeof(int) - 1 - i] = binaryReader.ReadByte();
			}
			return BitConverter.ToInt32(bytes, 0);
		}

		#endregion

		#region BASE 64
		public static string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				// Convert Image to byte[]
				image.Save(ms, format);
				byte[] imageBytes = ms.ToArray();

				// Convert byte[] to Base64 String
				string base64String = Convert.ToBase64String(imageBytes);
				return base64String;
			}
		}

		public static Image ImageFromBase64(string base64String)
		{
			// Convert Base64 String to byte[]
			byte[] imageBytes = Convert.FromBase64String(base64String);
			MemoryStream ms = new MemoryStream(imageBytes, 0,
			  imageBytes.Length);

			// Convert byte[] to Image
			ms.Write(imageBytes, 0, imageBytes.Length);
			Image image = Image.FromStream(ms, true);
			return image;
		}
		#endregion

		#region BLUR
		public static Bitmap Blur(Bitmap input, int radius)
		{
			if (radius < 1) radius = 1;

			BitmapData sourceData = input.LockBits(new Rectangle(0, 0,
									 input.Width, input.Height),
													   ImageLockMode.ReadOnly,
												 PixelFormat.Format32bppArgb);

			byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
			byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];

			Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
			input.UnlockBits(sourceData);

			double blue = 0.0;
			double green = 0.0;
			double red = 0.0;

			int filterWidth = radius;
			int filterHeight = radius;

			int filterOffset = (filterWidth - 1) / 2;
			int calcOffset = 0;

			int byteOffset = 0;

			for (int offsetY = filterOffset; offsetY <
				input.Height - filterOffset; offsetY++)
			{
				for (int offsetX = filterOffset; offsetX <
					input.Width - filterOffset; offsetX++)
				{
					blue = 0;
					green = 0;
					red = 0;

					byteOffset = offsetY *
								 sourceData.Stride +
								 offsetX * 4;

					int count = 0;

					for (int filterY = -filterOffset;
						filterY <= filterOffset; filterY++)
					{
						for (int filterX = -filterOffset;
							filterX <= filterOffset; filterX++)
						{
							calcOffset = byteOffset +
										 (filterX * 4) +
										 (filterY * sourceData.Stride);

							blue += (double)(pixelBuffer[calcOffset]);

							green += (double)(pixelBuffer[calcOffset + 1]);

							red += (double)(pixelBuffer[calcOffset + 2]);

							count++;
						}
					}

					blue /= count;
					green /= count;
					red /= count;

					blue = (blue > 255 ? 255 :
						   (blue < 0 ? 0 :
							blue));

					green = (green > 255 ? 255 :
							(green < 0 ? 0 :
							 green));

					red = (red > 255 ? 255 :
						  (red < 0 ? 0 :
						   red));

					resultBuffer[byteOffset] = (byte)(blue);
					resultBuffer[byteOffset + 1] = (byte)(green);
					resultBuffer[byteOffset + 2] = (byte)(red);
					resultBuffer[byteOffset + 3] = 255;
				}
			}

			Bitmap resultBitmap = new Bitmap(input.Width, input.Height);

			BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
									 resultBitmap.Width, resultBitmap.Height),
													  ImageLockMode.WriteOnly,
												 PixelFormat.Format32bppArgb);

			Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
			resultBitmap.UnlockBits(resultData);

			return resultBitmap;
		}

		public enum BlurType
		{
			Mean3x3,
			Mean5x5,
			Mean7x7,
			Mean9x9,
			GaussianBlur3x3,
			GaussianBlur5x5,
			MotionBlur5x5,
			MotionBlur5x5At45Degrees,
			MotionBlur5x5At135Degrees,
			MotionBlur7x7,
			MotionBlur7x7At45Degrees,
			MotionBlur7x7At135Degrees,
			MotionBlur9x9,
			MotionBlur9x9At45Degrees,
			MotionBlur9x9At135Degrees,
			Median3x3,
			Median5x5,
			Median7x7,
			Median9x9,
			Median11x11
		}

		public static Bitmap ConvolutionFilter(Bitmap sourceBitmap,
												  double[,] filterMatrix,
													   double factor = 1,
															int bias = 0)
		{
			BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
									 sourceBitmap.Width, sourceBitmap.Height),
													   ImageLockMode.ReadOnly,
												 PixelFormat.Format32bppArgb);

			byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
			byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];

			Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
			sourceBitmap.UnlockBits(sourceData);

			double blue = 0.0;
			double green = 0.0;
			double red = 0.0;

			int filterWidth = filterMatrix.GetLength(1);
			int filterHeight = filterMatrix.GetLength(0);

			int filterOffset = (filterWidth - 1) / 2;
			int calcOffset = 0;

			int byteOffset = 0;

			for (int offsetY = filterOffset; offsetY <
				sourceBitmap.Height - filterOffset; offsetY++)
			{
				for (int offsetX = filterOffset; offsetX <
					sourceBitmap.Width - filterOffset; offsetX++)
				{
					blue = 0;
					green = 0;
					red = 0;

					byteOffset = offsetY *
								 sourceData.Stride +
								 offsetX * 4;

					for (int filterY = -filterOffset;
						filterY <= filterOffset; filterY++)
					{
						for (int filterX = -filterOffset;
							filterX <= filterOffset; filterX++)
						{
							calcOffset = byteOffset +
										 (filterX * 4) +
										 (filterY * sourceData.Stride);

							blue += (double)(pixelBuffer[calcOffset]) *
									filterMatrix[filterY + filterOffset,
														filterX + filterOffset];

							green += (double)(pixelBuffer[calcOffset + 1]) *
									 filterMatrix[filterY + filterOffset,
														filterX + filterOffset];

							red += (double)(pixelBuffer[calcOffset + 2]) *
								   filterMatrix[filterY + filterOffset,
													  filterX + filterOffset];
						}
					}

					blue = factor * blue + bias;
					green = factor * green + bias;
					red = factor * red + bias;

					blue = (blue > 255 ? 255 :
						   (blue < 0 ? 0 :
							blue));

					green = (green > 255 ? 255 :
							(green < 0 ? 0 :
							 green));

					red = (red > 255 ? 255 :
						  (red < 0 ? 0 :
						   red));

					resultBuffer[byteOffset] = (byte)(blue);
					resultBuffer[byteOffset + 1] = (byte)(green);
					resultBuffer[byteOffset + 2] = (byte)(red);
					resultBuffer[byteOffset + 3] = 255;
				}
			}

			Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

			BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
									 resultBitmap.Width, resultBitmap.Height),
													  ImageLockMode.WriteOnly,
												 PixelFormat.Format32bppArgb);

			Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
			resultBitmap.UnlockBits(resultData);

			return resultBitmap;
		}

		public static Bitmap MedianFilter(Bitmap sourceBitmap,
													int matrixSize)
		{
			BitmapData sourceData =
					   sourceBitmap.LockBits(new Rectangle(0, 0,
					   sourceBitmap.Width, sourceBitmap.Height),
					   ImageLockMode.ReadOnly,
					   PixelFormat.Format32bppArgb);

			byte[] pixelBuffer = new byte[sourceData.Stride *
										  sourceData.Height];

			byte[] resultBuffer = new byte[sourceData.Stride *
										   sourceData.Height];

			Marshal.Copy(sourceData.Scan0, pixelBuffer, 0,
									   pixelBuffer.Length);

			sourceBitmap.UnlockBits(sourceData);

			int filterOffset = (matrixSize - 1) / 2;
			int calcOffset = 0;

			int byteOffset = 0;

			List<int> neighbourPixels = new List<int>();
			byte[] middlePixel;

			for (int offsetY = filterOffset; offsetY <
				sourceBitmap.Height - filterOffset; offsetY++)
			{
				for (int offsetX = filterOffset; offsetX <
					sourceBitmap.Width - filterOffset; offsetX++)
				{
					byteOffset = offsetY *
								 sourceData.Stride +
								 offsetX * 4;

					neighbourPixels.Clear();

					for (int filterY = -filterOffset;
						filterY <= filterOffset; filterY++)
					{
						for (int filterX = -filterOffset;
							filterX <= filterOffset; filterX++)
						{

							calcOffset = byteOffset +
										 (filterX * 4) +
										 (filterY * sourceData.Stride);

							neighbourPixels.Add(BitConverter.ToInt32(
											 pixelBuffer, calcOffset));
						}
					}

					neighbourPixels.Sort();

					middlePixel = BitConverter.GetBytes(
									   neighbourPixels[filterOffset]);

					resultBuffer[byteOffset] = middlePixel[0];
					resultBuffer[byteOffset + 1] = middlePixel[1];
					resultBuffer[byteOffset + 2] = middlePixel[2];
					resultBuffer[byteOffset + 3] = middlePixel[3];
				}
			}

			Bitmap resultBitmap = new Bitmap(sourceBitmap.Width,
											 sourceBitmap.Height);

			BitmapData resultData =
					   resultBitmap.LockBits(new Rectangle(0, 0,
					   resultBitmap.Width, resultBitmap.Height),
					   ImageLockMode.WriteOnly,
					   PixelFormat.Format32bppArgb);

			Marshal.Copy(resultBuffer, 0, resultData.Scan0,
									   resultBuffer.Length);

			resultBitmap.UnlockBits(resultData);

			return resultBitmap;
		}

		public static Bitmap ImageBlurFilter(Bitmap sourceBitmap,
													BlurType blurType)
		{
			Bitmap resultBitmap = null;

			switch (blurType)
			{
				case BlurType.Mean3x3:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
									   BlurMatrix.Mean3x3, 1.0 / 9.0, 0);
					} break;
				case BlurType.Mean5x5:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
									   BlurMatrix.Mean5x5, 1.0 / 25.0, 0);
					} break;
				case BlurType.Mean7x7:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
									   BlurMatrix.Mean7x7, 1.0 / 49.0, 0);
					} break;
				case BlurType.Mean9x9:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
									   BlurMatrix.Mean9x9, 1.0 / 81.0, 0);
					} break;
				case BlurType.GaussianBlur3x3:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
								BlurMatrix.GaussianBlur3x3, 1.0 / 16.0, 0);
					} break;
				case BlurType.GaussianBlur5x5:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
							   BlurMatrix.GaussianBlur5x5, 1.0 / 159.0, 0);
					} break;
				case BlurType.MotionBlur5x5:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
								   BlurMatrix.MotionBlur5x5, 1.0 / 10.0, 0);
					} break;
				case BlurType.MotionBlur5x5At45Degrees:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
						BlurMatrix.MotionBlur5x5At45Degrees, 1.0 / 5.0, 0);
					} break;
				case BlurType.MotionBlur5x5At135Degrees:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
						BlurMatrix.MotionBlur5x5At135Degrees, 1.0 / 5.0, 0);
					} break;
				case BlurType.MotionBlur7x7:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
						BlurMatrix.MotionBlur7x7, 1.0 / 14.0, 0);
					} break;
				case BlurType.MotionBlur7x7At45Degrees:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
						BlurMatrix.MotionBlur7x7At45Degrees, 1.0 / 7.0, 0);
					} break;
				case BlurType.MotionBlur7x7At135Degrees:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
						BlurMatrix.MotionBlur7x7At135Degrees, 1.0 / 7.0, 0);
					} break;
				case BlurType.MotionBlur9x9:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
						BlurMatrix.MotionBlur9x9, 1.0 / 18.0, 0);
					} break;
				case BlurType.MotionBlur9x9At45Degrees:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
						BlurMatrix.MotionBlur9x9At45Degrees, 1.0 / 9.0, 0);
					} break;
				case BlurType.MotionBlur9x9At135Degrees:
					{
						resultBitmap = ConvolutionFilter(sourceBitmap,
						BlurMatrix.MotionBlur9x9At135Degrees, 1.0 / 9.0, 0);
					} break;
				case BlurType.Median3x3:
					{
						resultBitmap = MedianFilter(sourceBitmap, 3);
					} break;
				case BlurType.Median5x5:
					{
						resultBitmap = MedianFilter(sourceBitmap, 5);
					} break;
				case BlurType.Median7x7:
					{
						resultBitmap = MedianFilter(sourceBitmap, 7);
					} break;
				case BlurType.Median9x9:
					{
						resultBitmap = MedianFilter(sourceBitmap, 9);
					} break;
				case BlurType.Median11x11:
					{
						resultBitmap = MedianFilter(sourceBitmap, 11);
					} break;
			}

			return resultBitmap;
		}

		public static class BlurMatrix
		{
			public static double[,] Mean3x3
			{
				get
				{
					return new double[,]   
                { { 1, 1, 1, },  
                  { 1, 1, 1, },  
                  { 1, 1, 1, }, };
				}
			}

			public static double[,] Mean5x5
			{
				get
				{
					return new double[,]   
                { { 1, 1, 1, 1, 1},  
                  { 1, 1, 1, 1, 1}, 
                  { 1, 1, 1, 1, 1}, 
                  { 1, 1, 1, 1, 1}, 
                  { 1, 1, 1, 1, 1}, };
				}
			}

			public static double[,] Mean7x7
			{
				get
				{
					return new double[,]   
                { { 1, 1, 1, 1, 1, 1, 1},  
                  { 1, 1, 1, 1, 1, 1, 1}, 
                  { 1, 1, 1, 1, 1, 1, 1}, 
                  { 1, 1, 1, 1, 1, 1, 1}, 
                  { 1, 1, 1, 1, 1, 1, 1}, 
                  { 1, 1, 1, 1, 1, 1, 1}, 
                  { 1, 1, 1, 1, 1, 1, 1}, };
				}
			}

			public static double[,] Mean9x9
			{
				get
				{
					return new double[,]   
                { { 1, 1, 1, 1, 1, 1, 1, 1, 1},  
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                  { 1, 1, 1, 1, 1, 1, 1, 1, 1}, };
				}
			}

			public static double[,] GaussianBlur3x3
			{
				get
				{
					return new double[,]   
                { { 1, 2, 1, },  
                  { 2, 4, 2, },  
                  { 1, 2, 1, }, };
				}
			}

			public static double[,] GaussianBlur5x5
			{
				get
				{
					return new double[,]   
                { { 2, 04, 05, 04, 2 },  
                  { 4, 09, 12, 09, 4 },  
                  { 5, 12, 15, 12, 5 }, 
                  { 4, 09, 12, 09, 4 }, 
                  { 2, 04, 05, 04, 2 }, };
				}
			}

			public static double[,] MotionBlur5x5
			{
				get
				{
					return new double[,]   
                { { 1, 0, 0, 0, 1},  
                  { 0, 1, 0, 1, 0},  
                  { 0, 0, 1, 0, 0}, 
                  { 0, 1, 0, 1, 0}, 
                  { 1, 0, 0, 0, 1}, };
				}
			}

			public static double[,] MotionBlur5x5At45Degrees
			{
				get
				{
					return new double[,]   
                { { 0, 0, 0, 0, 1},  
                  { 0, 0, 0, 1, 0},  
                  { 0, 0, 1, 0, 0}, 
                  { 0, 1, 0, 0, 0}, 
                  { 1, 0, 0, 0, 0}, };
				}
			}

			public static double[,] MotionBlur5x5At135Degrees
			{
				get
				{
					return new double[,]   
                { { 1, 0, 0, 0, 0},  
                  { 0, 1, 0, 0, 0},  
                  { 0, 0, 1, 0, 0}, 
                  { 0, 0, 0, 1, 0}, 
                  { 0, 0, 0, 0, 1}, };
				}
			}

			public static double[,] MotionBlur7x7
			{
				get
				{
					return new double[,]   
                { { 1, 0, 0, 0, 0, 0, 1},  
                  { 0, 1, 0, 0, 0, 1, 0},  
                  { 0, 0, 1, 0, 1, 0, 0}, 
                  { 0, 0, 0, 1, 0, 0, 0}, 
                  { 0, 0, 1, 0, 1, 0, 0}, 
                  { 0, 1, 0, 0, 0, 1, 0}, 
                  { 1, 0, 0, 0, 0, 0, 1}, };
				}
			}

			public static double[,] MotionBlur7x7At45Degrees
			{
				get
				{
					return new double[,]   
                { { 0, 0, 0, 0, 0, 0, 1},  
                  { 0, 0, 0, 0, 0, 1, 0},  
                  { 0, 0, 0, 0, 1, 0, 0}, 
                  { 0, 0, 0, 1, 0, 0, 0}, 
                  { 0, 0, 1, 0, 0, 0, 0}, 
                  { 0, 1, 0, 0, 0, 0, 0}, 
                  { 1, 0, 0, 0, 0, 0, 0}, };
				}
			}

			public static double[,] MotionBlur7x7At135Degrees
			{
				get
				{
					return new double[,]   
                { { 1, 0, 0, 0, 0, 0, 0},  
                  { 0, 1, 0, 0, 0, 0, 0},  
                  { 0, 0, 1, 0, 0, 0, 0}, 
                  { 0, 0, 0, 1, 0, 0, 0}, 
                  { 0, 0, 0, 0, 1, 0, 0}, 
                  { 0, 0, 0, 0, 0, 1, 0}, 
                  { 0, 0, 0, 0, 0, 0, 1}, };
				}
			}

			public static double[,] MotionBlur9x9
			{
				get
				{
					return new double[,]   
                { {1, 0, 0, 0, 0, 0, 0, 0, 1,}, 
                  {0, 1, 0, 0, 0, 0, 0, 1, 0,}, 
                  {0, 0, 1, 0, 0, 0, 1, 0, 0,}, 
                  {0, 0, 0, 1, 0, 1, 0, 0, 0,}, 
                  {0, 0, 0, 0, 1, 0, 0, 0, 0,}, 
                  {0, 0, 0, 1, 0, 1, 0, 0, 0,}, 
                  {0, 0, 1, 0, 0, 0, 1, 0, 0,}, 
                  {0, 1, 0, 0, 0, 0, 0, 1, 0,}, 
                  {1, 0, 0, 0, 0, 0, 0, 0, 1,}, };
				}
			}

			public static double[,] MotionBlur9x9At45Degrees
			{
				get
				{
					return new double[,]   
                { {0, 0, 0, 0, 0, 0, 0, 0, 1,}, 
                  {0, 0, 0, 0, 0, 0, 0, 1, 0,}, 
                  {0, 0, 0, 0, 0, 0, 1, 0, 0,}, 
                  {0, 0, 0, 0, 0, 1, 0, 0, 0,}, 
                  {0, 0, 0, 0, 1, 0, 0, 0, 0,}, 
                  {0, 0, 0, 1, 0, 0, 0, 0, 0,}, 
                  {0, 0, 1, 0, 0, 0, 0, 0, 0,}, 
                  {0, 1, 0, 0, 0, 0, 0, 0, 0,}, 
                  {1, 0, 0, 0, 0, 0, 0, 0, 0,}, };
				}
			}

			public static double[,] MotionBlur9x9At135Degrees
			{
				get
				{
					return new double[,]   
                { {1, 0, 0, 0, 0, 0, 0, 0, 0,}, 
                  {0, 1, 0, 0, 0, 0, 0, 0, 0,}, 
                  {0, 0, 1, 0, 0, 0, 0, 0, 0,}, 
                  {0, 0, 0, 1, 0, 0, 0, 0, 0,}, 
                  {0, 0, 0, 0, 1, 0, 0, 0, 0,}, 
                  {0, 0, 0, 0, 0, 1, 0, 0, 0,}, 
                  {0, 0, 0, 0, 0, 0, 1, 0, 0,}, 
                  {0, 0, 0, 0, 0, 0, 0, 1, 0,}, 
                  {0, 0, 0, 0, 0, 0, 0, 0, 1,}, };
				}
			}
		}
		#endregion

		public static void FloodFill(Bitmap bitmap, int x, int y, Color color, int threshold = 1)
		{
			BitmapData data = bitmap.LockBits(
				new Rectangle(0, 0, bitmap.Width, bitmap.Height),
				ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
			int[] bits = new int[data.Stride / 4 * data.Height];
			Marshal.Copy(data.Scan0, bits, 0, bits.Length);

			LinkedList<Point> check = new LinkedList<Point>();
			int floodTo = color.ToArgb();
			int floodFrom = bits[x + y * data.Stride / 4];
			bits[x + y * data.Stride / 4] = floodTo;

			if (floodFrom != floodTo)
			{
				check.AddLast(new Point(x, y));
				while (check.Count > 0)
				{
					Point cur = check.First.Value;
					check.RemoveFirst();

					foreach (Point off in new Point[] {
                new Point(0, -1), new Point(0, 1), 
                new Point(-1, 0), new Point(1, 0)})
					{
						Point next = new Point(cur.X + off.X, cur.Y + off.Y);
						if (next.X >= 0 && next.Y >= 0 &&
							next.X < data.Width &&
							next.Y < data.Height)
						{
							if (Math.Abs(bits[next.X + next.Y * data.Stride / 4] - floodFrom) < (1 + (threshold - 1) * 400000))
							{
								check.AddLast(next);
								bits[next.X + next.Y * data.Stride / 4] = floodTo;
							}
						}
					}
				}
			}

			Marshal.Copy(bits, 0, data.Scan0, bits.Length);
			bitmap.UnlockBits(data);
		}
	}

	public delegate void ProgressEventHandler(object sender, ProgressEventArgs e);
	public class ProgressEventArgs : EventArgs
	{
		public int Progress { get; set; }
		public ProgressEventArgs(int progress)
		{
			this.Progress = progress;
		}
	}
}
