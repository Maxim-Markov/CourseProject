using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using CourseProject.windows;

namespace CourseProject.Other_classes
{
    public class KeyChecker
    {
        //проверяет корректность введённого в окно KeyWindow ключа и не позволяет пользователю вводить недопустимые символы
        public static void CheckKey(object sender, ICollection<TextChange> changes)
        {
            KeyWindow window = sender as KeyWindow;
            GradientStop correct = new GradientStop(Colors.Green, 0.5);
            GradientStop error = new GradientStop(Colors.Red, 0.5);
            if (window.FullKeyBox.Text.Length == 0)
            {
                window.KeyErrors.Text = "Пустой ключ недопустим";

                window.BackgroudGrad.GradientStops.RemoveAt(4);
                window.BackgroudGrad.GradientStops.Insert(4, error);

            }

            foreach (var item in changes)
            {
                if (item.AddedLength == 0)
                {
                    if ((window.FullKeyBox.Text != "")&&(item.RemovedLength > 0))
                    {
                        window.KeyErrors.Text = "";
                        window.BackgroudGrad.GradientStops.RemoveAt(4);
                        window.BackgroudGrad.GradientStops.Insert(4, correct);
                    }
                    return;
                }
                else
                {
                    for (int i = item.Offset; i < (item.Offset + item.AddedLength); i++)
                    {
                        char insertedSymb = window.FullKeyBox.Text[i];
                        if (!TextEncoder.RusAlphabet.Contains(insertedSymb) && !TextEncoder.RusUpperAlphabet.Contains(insertedSymb))
                        {
                            window.FullKeyBox.Text = window.FullKeyBox.Text.Remove(item.Offset, item.AddedLength);
                            window.FullKeyBox.CaretIndex = window.FullKeyBox.Text.Length;
                            window.KeyErrors.Text = "Попытка ввести неверный символ пресечена";
                            window.BackgroudGrad.GradientStops.RemoveAt(4);
                            window.BackgroudGrad.GradientStops.Insert(4, error);
                            return;
                        }
                        window.KeyErrors.Text = "";
                        window.BackgroudGrad.GradientStops.RemoveAt(4);
                        window.BackgroudGrad.GradientStops.Insert(4, correct);
                    }
                }
            }
        }
    }
}
