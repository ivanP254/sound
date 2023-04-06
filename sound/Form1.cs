using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const string fileInvalid = "файл не является подходящим .wav файлом";
        private const string fileNotExist = "файл не существует";
        

        

        private unsafe void button_play_Click(object sender, EventArgs e)
        {


            string path = $"sounds/{text_box_path.Text}.wav";

            

            if (File.Exists(path))
            {
                //*/
                byte[] vawFile = File.ReadAllBytes(path);

                byte[] header = new byte[44];
                Array.Copy(vawFile, header, 44);

                
                
                // проверка заголовка файла на принадлежность к вав

                // байты [0-3] - надпись 'RIFF'
                if(header[0] != 82 || header[1] != 73 || header[2] != 70 || header[3] != 70)
                {
                    label_info.Text = fileInvalid;
                    return;
                }

                // байты [8-11] - надпись 'WAVE'
                if (header[8] != 87 || header[9] != 65 || header[10] != 86 || header[11] != 69)
                {
                    label_info.Text = fileInvalid;
                    return;
                }

                // байты [12-15] - надпись 'fmt '
                if (header[12] != 102 || header[13] != 109 || header[14] != 116 || header[15] != 32)
                {
                    label_info.Text = fileInvalid;
                    return;
                }

                // байты [16-19] - размер сегмента (?) (16 = PCM?)
                if (header[16] != 16)
                {
                    label_info.Text = fileInvalid;
                    return;
                }

                // байты [20-21] - уровень сжатия (1 = PCM)
                if (header[20] != 1)
                {
                    label_info.Text = fileInvalid;
                    return;
                }

                // байты [22-23] - количество каналов (моно/стерео)
                if (!(header[22] == 1 || header[22] == 2))
                {
                    label_info.Text = fileInvalid;
                    return;
                }
                int channels = header[22];
                
                // байты [34-35] - количество битов на семпл
                ushort bitsPerSample;
                fixed (byte* b = &header[34])
                {
                    bitsPerSample = *(ushort*)b;
                }
                ushort bytesPerSample = (ushort)(bitsPerSample / 8);

                // байты [36-39] - надпись 'data'
                if (header[36] != 100 || header[37] != 97 || header[38] != 116 || header[39] != 97)
                {
                    label_info.Text = fileInvalid;
                    return;
                }

                // байты [40-43] - количество байтов данных
                uint size;
                fixed (byte* b = &header[40])
                {
                    size = *(uint*)b;
                }



                // работа с данными
                byte[] data = new byte[size];
                Array.Copy(vawFile, 44, data, 0, size);


                int speedRate = (int)input_duration_rate.Value;

                // уменьшить/увеличить в ноль раз - нельзя
                if(speedRate == 0)
                {
                    label_info.Text = "неправильное значение изменения длительности";
                    return;
                }

                byte[] newData;
                string newPath;


                // усли нужно уменьшить длину
                if (speedRate < 0)
                {
                    newData = decreaceDuration(data, bytesPerSample, channels, -speedRate);
                    newPath = $"sounds/{text_box_path.Text}_decreased_x{-speedRate}.wav";
                }
                // если нужно увеличить длину
                else
                {
                    newData = increaseDuration(data, bytesPerSample, channels, speedRate);
                    newPath = $"sounds/{text_box_path.Text}_increased_x{speedRate}.wav";
                }

                // редактируем заголовок

                // изменям "размер файла" (4-7)
                fixed (byte* b = &header[4])
                {
                    *(int*)b = newData.Length + 36;
                }

                // изменяем "размер данных" (40-43)
                fixed (byte* b = &header[40])
                {
                    *(int*)b = newData.Length;
                }


                // сохраняем в новую переменную
                var result = new byte[header.Length + newData.Length];
                header.CopyTo(result, 0);
                newData.CopyTo(result, header.Length);


                // сохраняем в новый вав-файл
                using (FileStream fs = File.Create(newPath))
                {
                    fs.Write(result, 0, result.Length);
                }

                //*/

                //проигрываем новый звук
                SoundPlayer simpleSound = new SoundPlayer(newPath);
                simpleSound.Play();


            }
            else
            {
                label_info.Text = fileNotExist;
                return;
            }
            

            label_info.Text = "all ok";
            
        }

        private byte[] decreaceDuration(byte[] array, ushort bps, int chans, int decreaceRate)
        {
            int size = array.Length / chans / bps;
            int sampleSize = bps * chans;
            int newSize = size / decreaceRate;

            byte[] toRet = new byte[newSize * chans * bps];

            for (int i = 0; i < newSize; i++)
            {
                Array.Copy(array, i * sampleSize * decreaceRate, toRet, i * sampleSize, sampleSize);
            }
            return toRet;
        }

        private unsafe byte[] increaseDuration(byte[] array, ushort bps, int chans, int increaseRate)
        {
            int sampleSize = bps * chans; // размер одного семпла
            int newSampleChunkSize = sampleSize * increaseRate;

            byte[] toRet = new byte[(array.Length - sampleSize) * increaseRate]; // размер итогового массива - теряется последний семпл из оригинального массива


            // перебираем все семплы в массиве кроме последнего
            for(int i = 0; i < (array.Length / sampleSize) - 1; i++)
            {
                // отдельно смотрим для каждого канала
                for (int c = 0; c < chans; c++)
                {

                    // берем указатели на текущий элемент текущего канала и на следующий элемент текущего канала из оригинального массива
                    fixed (byte* begin = &array[i * sampleSize + c * bps], end = &array[(i + 1) * sampleSize + c * bps])
                    {
                        // теперь надо пройтись по всем тем элементам между этими двумея, которые мы создаем
                        for(int j = 0; j < increaseRate; j++)
                        {
                            // берем указатель на тот элемент в новом массиве, который заполняем, с учетом канала
                            fixed (byte* elem = &toRet[i * newSampleChunkSize + j * sampleSize + c * bps])
                            {
                                // заполняем этот элемент
                                // если 8-bit аудио
                                if(bps == 1)
                                {
                                    *elem = (byte)(*begin + j * (*end - *begin) / increaseRate);
                                }
                                // если 16-bit аудио
                                if(bps == 2)
                                {
                                    *(short*)elem = (short)(*(short*)begin + j * (*(short*)end - *(short*)begin) / increaseRate);
                                }
                            }
                        }
                    }


                }
            }

    
            return toRet;
        }

    }
}
