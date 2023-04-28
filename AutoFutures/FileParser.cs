using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace AutoFutures
{
    class ParserManager
    {
        private static ParserManager Instance;
        public static ParserManager getInstance()
        {
            if(Instance == null)
            {
                Instance = new ParserManager();
            }
            return Instance;
        }
        
        public Dictionary<string, FileParser> m_pParsers;
        private ParserManager()
        {
            m_pParsers = new Dictionary<string, FileParser>();
        }
    }
    class FileParser
    {
        private string m_pPath;
        Dictionary<string, string> m_pKeyValues;
        public List<string> m_pKeys;
        public FileParser(string path)
        {
            m_pPath = path;
            m_pKeyValues = new Dictionary<string, string>();
            m_pKeys = new List<string>();
        }

        public void StartParse()
        {
            try
            {
                using (StreamReader sr = new StreamReader(m_pPath))
                {
                    string line;                  
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line == "")
                        {
                            continue;
                        }
                        string[] dict = line.Split('=');
                        if(dict.Length != 2)
                            continue;

                        foreach (var key in m_pKeys)
                        {
                            if (dict[0].Equals(key))
                            {
                                m_pKeyValues.Add(key, dict[1]);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (System.Windows.MessageBox.Show("Setting有問題，請洽工程師", "提示：",
                            MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    //TODO:根據不同狀況控制
                }
            }
          
        }
    }
}
