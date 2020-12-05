using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using System.Windows;
using System.Reflection;

namespace CourseProject.Other_classes
{
    //каждый объект класса ассоциирован со своим собственным приложением Word и документом в нём
    public class WordHandler
    {
        private Word.Application wordapp;
        private Word.Document worddocument;
        //открывает документ по указанному пути
        public bool OpenWord(string fullpath,bool isReadonly, bool isVisible)
        {
            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */
            wordapp = new Word.Application();
            wordapp.Visible=isVisible;
            Object filename = fullpath;
            Object confirmConversions = false;
            Object readOnly = isReadonly;
            Object addToRecentFiles = false;
            Object passwordDocument = Type.Missing;
            Object passwordTemplate = Type.Missing;
            Object revert = false;
            Object writePasswordDocument = Type.Missing;
            Object writePasswordTemplate = Type.Missing;
            Object format = Type.Missing;
            Object encoding = Type.Missing;
            Object oVisible = Type.Missing;
            Object openConflictDocument = Type.Missing;
            Object openAndRepair = Type.Missing;
            Object documentDirection = Type.Missing;
            Object noEncodingDialog = false;
            Object xmlTransform = Type.Missing;
            try
            {
#if OFFICEXP//если установлен OfficeXp
               worddocument = wordapp.Documents.Open2000(ref filename,
            ref confirmConversions, ref readOnly, ref addToRecentFiles,
          ref passwordDocument, ref passwordTemplate, ref revert,
          ref writePasswordDocument, ref writePasswordTemplate,
          ref format, ref encoding, ref oVisible,
          ref openAndRepair, ref documentDirection, ref noEncodingDialog, ref xmlTransform);
#else
                worddocument = wordapp.Documents.Open(ref filename,
              ref confirmConversions, ref readOnly, ref addToRecentFiles,
              ref passwordDocument, ref passwordTemplate, ref revert,
              ref writePasswordDocument, ref writePasswordTemplate,
              ref format, ref encoding, ref oVisible,
              ref openAndRepair, ref documentDirection, ref noEncodingDialog, ref xmlTransform);
#endif
                return true;
            }
            catch(Exception ex)
            {
                wordapp = null;
                worddocument = null;
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //закрывает приложение для объекта текущего класса
        public bool CloseWord()
        {
            bool isSuccess = true;
            Object saveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;
            Object originalFormat = Word.WdOriginalFormat.wdOriginalDocumentFormat;
            Object routeDocument = Type.Missing;
            try
            {
                if (wordapp != null)
                {
                    wordapp.Quit(ref saveChanges,
                                 ref originalFormat, ref routeDocument);
                }
            }
            catch (Exception ex)
            {
                
                isSuccess = false;
            }
            finally
            {
                wordapp = null;
            }
            return isSuccess;
        }
        //записывает текст из текущего документа в массив строк - каждый абзац с новой строки
        public string[] GetAllText()
        {
            Word.Paragraphs wordparagraphs = null;
            try
            {
                wordparagraphs = worddocument.Paragraphs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            string[] allText = null;
             if ((wordparagraphs != null) && (wordparagraphs.Count > 0))
            {
                 allText = new string[wordparagraphs.Count];
            }
            else
            {
                return null;
            }
                
            for (int i = 1; i <= wordparagraphs.Count; i++)
            {
                Word.Paragraph wordparagraph = (Word.Paragraph)wordparagraphs[i];
                allText[i - 1] = wordparagraph.Range.Text;
            }
            return allText;
        }
        //записывает текст из массива строк в документ, каждый элемент массива - новый абзац
        public bool WriteAllText(string[] content)
        {
            //Получаем ссылки на параграфы документа
            worddocument.Content.Font.Size = 14;
            worddocument.Content.Font.Bold = 0;
            worddocument.Content.Font.Name = "Times New Roman";
            worddocument.Content.Font.Color = Word.WdColor.wdColorBlack;
            worddocument.Content.ParagraphFormat.Alignment =
            Word.WdParagraphAlignment.wdAlignParagraphJustify;

            try
            {
                Word.Paragraphs wordparagraphs = worddocument.Paragraphs;
                for (int i = 0; i < content.Length; i++)
                {
                    object oMissing = Missing.Value;
                    worddocument.Paragraphs.Add(ref oMissing);
                    Word.Paragraph wordparagraph = (Word.Paragraph)wordparagraphs[i + 1];
                    wordparagraph.Range.Text = content[i];
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }
        //открывает приложение Word и создаёт новый документ
        public bool CreateDocument()
        {
            Object template = Type.Missing;
            Object newTemplate = false;
            Object documentType = Word.WdNewDocumentType.wdNewBlankDocument;
            Object visible = true;
            try
            {
                wordapp = new Word.Application();
                wordapp.Visible = false;
                worddocument = wordapp.Documents.Add(
    ref template, ref newTemplate, ref documentType, ref visible);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }
        //сохраняет документ
        public bool SaveDocument(string fullpath)
        {
            Object fileName = fullpath;
            Object fileFormat = Type.Missing;
            Object lockComments = Type.Missing;
            Object password = Type.Missing;
            Object addToRecentFiles = Type.Missing;
            Object writePassword = Type.Missing;
            Object readOnlyRecommended = Type.Missing;
            Object embedTrueTypeFonts = Type.Missing;
            Object saveNativePictureFormat = Type.Missing;
            Object saveFormsData = Type.Missing;
            Object saveAsAOCELetter = Type.Missing;
            Object encoding = Type.Missing;
            Object insertLineBreaks = Type.Missing;
            Object allowSubstitutions = Type.Missing;
            Object lineEnding = Type.Missing;
            Object addBiDiMarks = Type.Missing;

            try
            {
                worddocument.SaveAs2(ref fileName,
               ref fileFormat, ref lockComments,
               ref password, ref addToRecentFiles, ref writePassword,
               ref readOnlyRecommended, ref embedTrueTypeFonts,
               ref saveNativePictureFormat, ref saveFormsData,
               ref saveAsAOCELetter, ref encoding, ref insertLineBreaks,
               ref allowSubstitutions, ref lineEnding, ref addBiDiMarks, Type.Missing);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }
        ~WordHandler()
        {
            CloseWord();
            worddocument = null;
            wordapp = null;
        }
    }
}
