using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Bomeans.IRNet;

namespace BomeansPCTool
{
    class SettingFiles
    {
        private static String fAppPath = Path.GetDirectoryName(Application.ExecutablePath);
        private const String KEY_LIST_FILE_NAME = "keyList.txt";
        private const String TYPE_LIST_FILE_NAME = "typeList.txt";
        private const String BRAND_LIST_FILE_NAME = "brandList_{0}.txt";

        private static String GetFilePath(String fileName)
        {
            String rootFolder = String.Format(@"{0}\support", fAppPath);

            try
            {
                if (!Directory.Exists(rootFolder))
                {
                    Directory.CreateDirectory(rootFolder);
                }

                return String.Format(@"{0}\{1}", rootFolder, fileName);
            }
            catch
            {
                return null;
            }
        }

        private static String GetKeyListFilePath()
        {
            return GetFilePath(KEY_LIST_FILE_NAME);
        }

        private static String GetTypeListFilePath()
        {
            return GetFilePath(TYPE_LIST_FILE_NAME);
        }

        private static String GetBrandListFilePath(String typeId)
        {
            return GetFilePath(String.Format(BRAND_LIST_FILE_NAME, typeId));
        }

        public static Boolean SaveKeyListToFile(KeyName[] keys)
        {
            if (keys == null || keys.Length == 0)
            {
                return false;
            }

            String filePath = GetKeyListFilePath();

            String[] lines = new String[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                lines[i] = String.Format("{0},{1},{2}", keys[i].TypeId, keys[i].Id, keys[i].LocalizedName);
            }

            try
            {
                File.WriteAllLines(filePath, lines);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static KeyName[] LoadKeyListFromFile()
        {
            String filePath = GetKeyListFilePath();
            String[] segments;
            if (File.Exists(filePath))
            {
                String[] lines = File.ReadAllLines(filePath);

                if (lines.Length > 0)
                {
                    List<KeyName> keyList = new List<KeyName>();

                    foreach (String line in lines)
                    {
                        segments = line.Split(new char[] { ',' });
                        if (segments.Length >= 3)
                        {
                            if ((segments[0].Trim().Length == 0) ||
                                (segments[1].Trim().Length == 0) ||
                                (segments[2].Trim().Length == 0))
                            {
                                continue;
                            }

                            keyList.Add(new KeyName(segments[0], segments[1], segments[2]));
                        }
                    }
                    return keyList.ToArray();
                }
            }

            return new KeyName[] { };
        }

        public static Boolean SaveTypeListToFile(TypeItem[] types)
        {
            if (types == null || types.Length == 0)
            {
                return false;
            }

            String filePath = GetTypeListFilePath();

            String[] lines = new String[types.Length];
            for (int i = 0; i < types.Length; i++)
            {
                lines[i] = String.Format("{0},{1},{2}", types[i].Id, types[i].LocalizedName, types[i].EnglishName);
            }

            try
            {
                File.WriteAllLines(filePath, lines);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static TypeItem[] LoadTypeListFromFile()
        {
            String filePath = GetTypeListFilePath();
            String[] segments;
            if (File.Exists(filePath))
            {
                String[] lines = File.ReadAllLines(filePath);

                if (lines.Length > 0)
                {
                    List<TypeItem> typeList = new List<TypeItem>();
                    foreach (String line in lines)
                    {
                        segments = line.Split(new char[] { ',' });
                        if (segments.Length >= 3)
                        {
                            typeList.Add(new TypeItem(segments[0], segments[1], segments[2]));
                        }
                    }
                    return typeList.ToArray();
                }
            }

            return new TypeItem[] { };
        }

        public static Boolean SaveBrandListToFile(String typeId, BrandItem[] brands)
        {
            if (typeId == null || brands == null || brands.Length == 0)
            {
                return false;
            }

            String filePath = GetBrandListFilePath(typeId);

            String[] lines = new String[brands.Length];
            for (int i = 0; i < brands.Length; i++)
            {
                lines[i] = String.Format("{0},{1},{2}", brands[i].Id, brands[i].LocalizedName, brands[i].EnglishName);
            }

            try
            {
                File.WriteAllLines(filePath, lines);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static BrandItem[] LoadBrandListFromFile(String typeId)
        {
            String filePath = GetBrandListFilePath(typeId);

            String[] segments;
            if (File.Exists(filePath))
            {
                String[] lines = File.ReadAllLines(filePath);

                if (lines.Length > 0)
                {
                    List<BrandItem> brandList = new List<BrandItem>();

                    foreach (String line in lines)
                    {
                        segments = line.Split(new char[] { ',' });
                        if (segments.Length >= 3)
                        {
                            brandList.Add(new BrandItem(segments[0], segments[1], segments[2]));
                        }
                    }
                    return brandList.ToArray();
                }
            }

            return new BrandItem[] { };
        }
    }
}
