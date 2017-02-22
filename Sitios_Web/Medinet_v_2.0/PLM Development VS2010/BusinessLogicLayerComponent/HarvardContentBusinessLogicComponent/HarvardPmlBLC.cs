using System;
using System.Collections.Generic;
using System.Text;

namespace HarvardContentBusinessLogicComponent
{
    public class HarvardPmlBLC
    {

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the HarvardPmlBLC class. Not receive parameters.
        /// </summary>
        public HarvardPmlBLC() { }
        #endregion

        #region Public Methods

        public List<HarvardContentBusinessEntries.HarvardPlmNewsInfo> getCompleteNew(string code, String title)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                _searchCriteriaType = new HarvardPlmNews.SearchCriteriaType();
                _titleType.Value = title;
                List<HarvardContentBusinessEntries.HarvardPlmNewsInfo> NewsList = new List<HarvardContentBusinessEntries.HarvardPlmNewsInfo>();
                HarvardContentBusinessEntries.HarvardPlmNewsInfo News = new HarvardContentBusinessEntries.HarvardPlmNewsInfo();
                _searchCriteriaType.IncludeBlocked = true;
                _searchCriteriaType.IncludeOriginalDocuments = true;
                _languageType.code = "en";
                _searchCriteriaType.Title = _titleType;
                _searchCriteriaType.Language = _languageType;
                _documentListType = _documentSearchBinding.DocumentSearch(_searchCriteriaType);
                if (_documentListType.Document != null)
                {
                    foreach (HarvardPlmNews.DocumentListTypeDocument doc in _documentListType.Document)
                    {
                        _documentRequest.ContentId = doc.ContentId;
                        _documentRequest.ContentTypeId = doc.ContentTypeId;
                        _documentType = _getDocumentBinding.GetDocument(_documentRequest);
                        _documentTypeContent = _documentType.Content;
                        News.datePublish = DateTime.Parse(_documentType.PublishedDate.ToString());
                        if (_documentType.RegularTitle.Text != null)
                        {
                            News.title = _documentType.RegularTitle.Text[0];
                        }

                        _bodyType = (HarvardPlmNews.bodyType)_documentTypeContent.Item;
                        _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.openTable;
                        for (int a = 0; a < _bodyType.Items.Length; a++)
                        {
                            if (_bodyType.ItemsElementName[a].ToString().Equals("Section"))
                            {
                                _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.openSection;
                                _sectionType = (HarvardPlmNews.SectionType)_bodyType.Items[a];
                                for (int b = 0; b < _sectionType.Items.Length; b++)
                                {
                                    if (_sectionType.Items[b].GetType().Name.Equals("tableType"))
                                    {
                                        _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.openTable;
                                        _tableType = (HarvardPlmNews.tableType)_sectionType.Items[b];
                                        foreach (HarvardPlmNews.tableSectionType item in _tableType.Items)
                                        {
                                            for (int t1 = 0; t1 < item.tr.Length; t1++)
                                            {
                                                _result += "<tr>";
                                                _trType = (HarvardPlmNews.trType)item.tr[t1];
                                                for (int tr1 = 0; tr1 < _trType.ItemsElementName.Length; tr1++)
                                                {
                                                    if (_trType.ItemsElementName[tr1].ToString().Equals("td"))
                                                    {
                                                        _result += "<td ";
                                                        _tdType = (HarvardPlmNews.tdType)_trType.Items[tr1];
                                                        _result += "rowspan= " + _tdType.rowspan + " ";
                                                        _result += "colspan= " + _tdType.colspan + " >";
                                                        for (int tds = 0; tds < _tdType.Items.Length; tds++)
                                                        {
                                                            if (_tdType.ItemsElementName[tds].ToString().Equals("h3"))
                                                            {
                                                                _result += HarvardContentBusinessEntries.HtmlInfo.Instance.openHead3;
                                                                _result += HarvardContentBusinessEntries.HtmlInfo.Instance.openParagraph;
                                                                _paragraphType = (HarvardPlmNews.paragraphType)_tdType.Items[tds];
                                                                _markupElementWithLinksType1 = (HarvardPlmNews.MarkupElementWithLinksType)(HarvardPlmNews.paragraphType)_paragraphType;
                                                                if (_markupElementWithLinksType1.Items != null)
                                                                {
                                                                    for (int c = 0; c < _markupElementWithLinksType1.Items.Length; c++)
                                                                    {
                                                                        try
                                                                        {
                                                                            _inlineType1 = (HarvardPlmNews.inlineType)_markupElementWithLinksType1.Items[c];
                                                                            _markupElementWithLinksType2 = (HarvardPlmNews.MarkupElementWithLinksType)_inlineType1;
                                                                            for (int d = 0; d < _markupElementWithLinksType2.Text.Length; d++)
                                                                            {
                                                                                _result = _result + _markupElementWithLinksType2.Text[d];
                                                                            }
                                                                        }
                                                                        catch (Exception)
                                                                        {

                                                                        }
                                                                    }
                                                                    if (_paragraphType.Text != null)
                                                                    {
                                                                        foreach (string it in _paragraphType.Text)
                                                                        {
                                                                            _result = _result + it;
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (_markupElementWithLinksType1.Text != null)
                                                                    {
                                                                        foreach (string txt in _markupElementWithLinksType1.Text)
                                                                        {
                                                                            _result = _result + txt;
                                                                        }
                                                                    }
                                                                }
                                                                _result += HarvardContentBusinessEntries.HtmlInfo.Instance.closeParagraph;
                                                                _result += HarvardContentBusinessEntries.HtmlInfo.Instance.closeHead3;
                                                            }
                                                            if (_tdType.ItemsElementName[tds].ToString().Equals("p"))
                                                            {
                                                                _result += HarvardContentBusinessEntries.HtmlInfo.Instance.openParagraph;
                                                                _paragraphType = (HarvardPlmNews.paragraphType)_tdType.Items[tds];
                                                                _markupElementWithLinksType1 = (HarvardPlmNews.MarkupElementWithLinksType)(HarvardPlmNews.paragraphType)_paragraphType;
                                                                if (_markupElementWithLinksType1.Items != null)
                                                                {
                                                                    for (int c = 0; c < _markupElementWithLinksType1.Items.Length; c++)
                                                                    {
                                                                        try
                                                                        {
                                                                            _inlineType1 = (HarvardPlmNews.inlineType)_markupElementWithLinksType1.Items[c];
                                                                            _markupElementWithLinksType2 = (HarvardPlmNews.MarkupElementWithLinksType)_inlineType1;
                                                                            for (int d = 0; d < _markupElementWithLinksType2.Text.Length; d++)
                                                                            {
                                                                                _result = _result + _markupElementWithLinksType2.Text[d];
                                                                            }
                                                                        }
                                                                        catch (Exception)
                                                                        {
                                                                        }
                                                                    }
                                                                    if (_paragraphType.Text != null)
                                                                    {
                                                                        foreach (string it in _paragraphType.Text)
                                                                        {
                                                                            _result = _result + it;
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (_markupElementWithLinksType1.Text != null)
                                                                    {
                                                                        foreach (string txt in _markupElementWithLinksType1.Text)
                                                                        {
                                                                            _result = _result + txt;
                                                                        }
                                                                    }
                                                                }
                                                                _result += HarvardContentBusinessEntries.HtmlInfo.Instance.closeParagraph;
                                                            }
                                                        }
                                                        _result += "</td>";
                                                    }
                                                }
                                                _result += "</tr>";
                                            }
                                        }
                                        _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeTable;
                                    }
                                    if (_sectionType.ItemsElementName[b].ToString().Equals("h1"))
                                    {
                                        _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.openHead1;
                                        if (_sectionType.Items[b].GetType().Name.Equals("paragraphType"))
                                        {
                                            _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.openParagraph;
                                            _paragraphType = (HarvardPlmNews.paragraphType)_sectionType.Items[b];
                                            _markupElementWithLinksType1 = (HarvardPlmNews.MarkupElementWithLinksType)(HarvardPlmNews.paragraphType)_paragraphType;
                                            if (_markupElementWithLinksType1.Items != null)
                                            {
                                                for (int c = 0; c < _markupElementWithLinksType1.Items.Length; c++)
                                                {
                                                    try
                                                    {
                                                        _inlineType1 = (HarvardPlmNews.inlineType)_markupElementWithLinksType1.Items[c];
                                                        _markupElementWithLinksType2 = (HarvardPlmNews.MarkupElementWithLinksType)_inlineType1;
                                                        if (_markupElementWithLinksType2.Text != null)
                                                        {
                                                            for (int d = 0; d < _markupElementWithLinksType2.Text.Length; d++)
                                                            {
                                                                _result = _result + _markupElementWithLinksType2.Text[d];
                                                            }
                                                        }
                                                    }
                                                    catch (Exception)
                                                    {
                                                    }
                                                }
                                                if (_paragraphType.Text != null)
                                                {
                                                    foreach (string it in _paragraphType.Text)
                                                    {
                                                        _result = _result + it;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (_markupElementWithLinksType1.Text != null)
                                                {
                                                    foreach (string txt in _markupElementWithLinksType1.Text)
                                                    {
                                                        _result = _result + txt;
                                                    }
                                                }
                                            }
                                            _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeParagraph;
                                        }
                                        _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeHead1;
                                    }
                                    if (_sectionType.ItemsElementName[b].ToString().Equals("Section"))
                                    {
                                        _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.openSection;
                                        _sectionTypeSub = (HarvardPlmNews.SectionType)_sectionType.Items[b];
                                        for (int e = 0; e < _sectionTypeSub.Items.Length; e++)
                                        {
                                            if (_sectionTypeSub.Items[e].GetType().Name.Equals("listULType"))
                                            {
                                                _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.openList;
                                                _listULType = (HarvardPlmNews.listULType)_sectionTypeSub.Items[e];
                                                foreach (HarvardPlmNews.HTMLBlockType item in _listULType.li)
                                                {
                                                    _htmlBlockType = (HarvardPlmNews.HTMLBlockType)item;
                                                    for (int z1 = 0; z1 < _htmlBlockType.Items.Length; z1++)
                                                    {
                                                        if (_htmlBlockType.ItemsElementName[z1].ToString().Equals("p"))
                                                        {
                                                            _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.openParagraph;
                                                            _paragraphType = (HarvardPlmNews.paragraphType)_htmlBlockType.Items[z1];
                                                            _markupElementWithLinksType1 = (HarvardPlmNews.MarkupElementWithLinksType)(HarvardPlmNews.paragraphType)_paragraphType;
                                                            if (_paragraphType.Items != null)
                                                            {
                                                                for (int f = 0; f < _markupElementWithLinksType1.Items.Length; f++)
                                                                {
                                                                    try
                                                                    {
                                                                        _inlineType1 = (HarvardPlmNews.inlineType)_markupElementWithLinksType1.Items[f];
                                                                        _markupElementWithLinksType2 = (HarvardPlmNews.MarkupElementWithLinksType)_inlineType1;
                                                                        if (_markupElementWithLinksType2.Text != null)
                                                                        {
                                                                            for (int g = 0; g < _markupElementWithLinksType2.Text.Length; g++)
                                                                            {
                                                                                _result = _result + _markupElementWithLinksType2.Text[g];
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeHead2;
                                                                            if (_markupElementWithLinksType2.Items != null)
                                                                            {
                                                                                for (int f1 = 0; f1 < _markupElementWithLinksType2.Items.Length; f1++)
                                                                                {
                                                                                    _inlineType2 = (HarvardPlmNews.inlineType)_markupElementWithLinksType2.Items[f1];
                                                                                    _markupElementWithLinksType3 = (HarvardPlmNews.MarkupElementWithLinksType)_inlineType2;
                                                                                    if (_markupElementWithLinksType3.Text != null)
                                                                                    {
                                                                                        for (int g1 = 0; g1 < _markupElementWithLinksType3.Text.Length; g1++)
                                                                                        {
                                                                                            _result = _result + _markupElementWithLinksType3.Text[g1];
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    catch (Exception)
                                                                    {
                                                                    }
                                                                }
                                                                if (_paragraphType.Text != null)
                                                                {
                                                                    foreach (string it in _paragraphType.Text)
                                                                    {
                                                                        _result = _result + it;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (_paragraphType.Text != null)
                                                                {
                                                                    foreach (string txt in _markupElementWithLinksType1.Text)
                                                                    {
                                                                        _result = _result + txt;
                                                                    }
                                                                }
                                                            }
                                                            _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeParagraph;
                                                        }
                                                    }
                                                }
                                                _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeList;
                                            }
                                            if (_sectionTypeSub.ItemsElementName[e].ToString().Equals("p"))
                                            {
                                                if (_sectionTypeSub.Items[e].GetType().Name.Equals("paragraphType"))
                                                {
                                                    _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.openParagraph;
                                                    _paragraphType = (HarvardPlmNews.paragraphType)_sectionTypeSub.Items[e];
                                                    _markupElementWithLinksType1 = (HarvardPlmNews.MarkupElementWithLinksType)(HarvardPlmNews.paragraphType)_paragraphType;
                                                    if (_paragraphType.Items != null)
                                                    {
                                                        for (int f = 0; f < _markupElementWithLinksType1.Items.Length; f++)
                                                        {
                                                            try
                                                            {

                                                                _inlineType1 = (HarvardPlmNews.inlineType)_markupElementWithLinksType1.Items[f];
                                                                _markupElementWithLinksType2 = (HarvardPlmNews.MarkupElementWithLinksType)_inlineType1;
                                                                if (_markupElementWithLinksType2.Text != null)
                                                                {
                                                                    for (int g = 0; g < _markupElementWithLinksType2.Text.Length; g++)
                                                                    {
                                                                        _result = _result + _markupElementWithLinksType2.Text[g];
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeHead2;
                                                                    if (_markupElementWithLinksType2.Items != null)
                                                                    {
                                                                        for (int f1 = 0; f1 < _markupElementWithLinksType2.Items.Length; f1++)
                                                                        {
                                                                            _inlineType2 = (HarvardPlmNews.inlineType)_markupElementWithLinksType2.Items[f1];
                                                                            _markupElementWithLinksType3 = (HarvardPlmNews.MarkupElementWithLinksType)_inlineType2;
                                                                            if (_markupElementWithLinksType3.Text != null)
                                                                            {
                                                                                for (int g1 = 0; g1 < _markupElementWithLinksType3.Text.Length; g1++)
                                                                                {
                                                                                    _result = _result + _markupElementWithLinksType3.Text[g1];
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            catch (Exception)
                                                            {
                                                            }
                                                        }
                                                        if (_paragraphType.Text != null)
                                                        {
                                                            foreach (string it in _paragraphType.Text)
                                                            {
                                                                _result = _result + it;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (_paragraphType.Text != null)
                                                        {
                                                            foreach (string txt in _markupElementWithLinksType1.Text)
                                                            {
                                                                _result = _result + txt;
                                                            }
                                                        }
                                                    }
                                                    _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeParagraph;
                                                }
                                            }
                                            if (_sectionTypeSub.ItemsElementName[e].ToString().Equals("h2"))
                                            {
                                                _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.openHead2;
                                                if (_sectionTypeSub.Items[e].GetType().Name.Equals("paragraphType"))
                                                {
                                                    _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.openParagraph;
                                                    _paragraphType = (HarvardPlmNews.paragraphType)_sectionTypeSub.Items[e];
                                                    _markupElementWithLinksType1 = (HarvardPlmNews.MarkupElementWithLinksType)(HarvardPlmNews.paragraphType)_paragraphType;
                                                    if (_paragraphType.Items != null)
                                                    {
                                                        for (int f = 0; f < _markupElementWithLinksType1.Items.Length; f++)
                                                        {
                                                            _inlineType1 = (HarvardPlmNews.inlineType)_markupElementWithLinksType1.Items[f];
                                                            _markupElementWithLinksType2 = (HarvardPlmNews.MarkupElementWithLinksType)_inlineType1;
                                                            if (_markupElementWithLinksType2.Text != null)
                                                            {
                                                                for (int g = 0; g < _markupElementWithLinksType2.Text.Length; g++)
                                                                {
                                                                    _result = _result + _markupElementWithLinksType2.Text[g];
                                                                }
                                                            }
                                                            else
                                                            {
                                                                _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeHead2;
                                                                if (_markupElementWithLinksType2.Items != null)
                                                                {
                                                                    for (int f1 = 0; f1 < _markupElementWithLinksType2.Items.Length; f1++)
                                                                    {
                                                                        _inlineType2 = (HarvardPlmNews.inlineType)_markupElementWithLinksType2.Items[f1];
                                                                        _markupElementWithLinksType3 = (HarvardPlmNews.MarkupElementWithLinksType)_inlineType2;
                                                                        if (_markupElementWithLinksType3.Text != null)
                                                                        {
                                                                            for (int g1 = 0; g1 < _markupElementWithLinksType3.Text.Length; g1++)
                                                                            {
                                                                                _result = _result + _markupElementWithLinksType3.Text[g1];
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        if (_paragraphType.Text != null)
                                                        {
                                                            foreach (string it in _paragraphType.Text)
                                                            {
                                                                _result = _result + it;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (_paragraphType.Text != null)
                                                        {
                                                            foreach (string txt in _markupElementWithLinksType1.Text)
                                                            {
                                                                _result = _result + txt;
                                                            }
                                                        }
                                                    }
                                                    _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeParagraph;
                                                }
                                                _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeHead2;
                                            }
                                            if (_sectionTypeSub.ItemsElementName[e].ToString().Equals("Section"))
                                            {
                                                _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.openSection;
                                                _sectionTypeSub_Sub = (HarvardPlmNews.SectionType)_sectionTypeSub.Items[e];
                                                for (int f = 0; f < _sectionTypeSub_Sub.Items.Length; f++)
                                                {
                                                    if (_sectionTypeSub_Sub.ItemsElementName[f].ToString().Equals("h3"))
                                                    {
                                                        _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.openHead3;
                                                        if (_sectionTypeSub_Sub.Items[f].GetType().Name.Equals("paragraphType"))
                                                        {
                                                            _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.openParagraph;
                                                            _paragraphType = (HarvardPlmNews.paragraphType)_sectionTypeSub_Sub.Items[f];
                                                            _markupElementWithLinksType1 = (HarvardPlmNews.MarkupElementWithLinksType)(HarvardPlmNews.paragraphType)_paragraphType;
                                                            if (_paragraphType.Items != null)
                                                            {
                                                                for (int g = 0; g < _markupElementWithLinksType1.Items.Length; g++)
                                                                {
                                                                    _inlineType1 = (HarvardPlmNews.inlineType)_markupElementWithLinksType1.Items[g];
                                                                    _markupElementWithLinksType2 = (HarvardPlmNews.MarkupElementWithLinksType)_inlineType1;
                                                                    for (int h = 0; h < _markupElementWithLinksType2.Text.Length; h++)
                                                                    {
                                                                        _result = _result + _markupElementWithLinksType2.Text[h];
                                                                    }
                                                                }
                                                                if (_paragraphType.Text != null)
                                                                {
                                                                    foreach (string it in _paragraphType.Text)
                                                                    {
                                                                        _result = _result + it;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (_paragraphType.Text != null)
                                                                {
                                                                    foreach (string txt in _markupElementWithLinksType1.Text)
                                                                    {
                                                                        _result = _result + txt;
                                                                    }
                                                                }
                                                            }
                                                            _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeParagraph;
                                                        }
                                                        _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeHead3;
                                                    }
                                                    if (_sectionTypeSub_Sub.ItemsElementName[f].ToString().Equals("p"))
                                                    {
                                                        if (_sectionTypeSub_Sub.Items[f].GetType().Name.Equals("paragraphType"))
                                                        {
                                                            _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.openParagraph;
                                                            _paragraphType = (HarvardPlmNews.paragraphType)_sectionTypeSub_Sub.Items[f];
                                                            _markupElementWithLinksType1 = (HarvardPlmNews.MarkupElementWithLinksType)(HarvardPlmNews.paragraphType)_paragraphType;
                                                            if (_paragraphType.Items != null)
                                                            {
                                                                for (int g1 = 0; g1 < _markupElementWithLinksType1.Items.Length; g1++)
                                                                {
                                                                    if (_markupElementWithLinksType1.Items[g1].GetType().Name.Equals("aType"))
                                                                    {
                                                                        _result += "<a href='" + ((HarvardPlmNews.aType)_markupElementWithLinksType1.Items[g1]).href + "'>";
                                                                        _result += ((HarvardPlmNews.aType)_markupElementWithLinksType1.Items[g1]).href;
                                                                        _result += "</a>";
                                                                    }
                                                                    else
                                                                    {
                                                                        _inlineType1 = (HarvardPlmNews.inlineType)_markupElementWithLinksType1.Items[g1];
                                                                        _markupElementWithLinksType2 = (HarvardPlmNews.MarkupElementWithLinksType)_inlineType1;
                                                                        for (int h1 = 0; h1 < _markupElementWithLinksType2.Text.Length; h1++)
                                                                        {
                                                                            _result = _result + _markupElementWithLinksType2.Text[h1];
                                                                        }
                                                                    }
                                                                }
                                                                if (_paragraphType.Text != null)
                                                                {
                                                                    foreach (string it in _paragraphType.Text)
                                                                    {
                                                                        _result = _result + it;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (_paragraphType.Text != null)
                                                                {
                                                                    foreach (string txt in _markupElementWithLinksType1.Text)
                                                                    {
                                                                        _result = _result + txt;
                                                                    }
                                                                }
                                                            }
                                                            _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeParagraph;
                                                        }
                                                    }
                                                }
                                                _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeSection;
                                            }
                                        }
                                        _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeSection;
                                    }
                                    if (_sectionType.ItemsElementName[b].ToString().Equals("p"))
                                    {
                                        if (_sectionType.Items[b].GetType().Name.Equals("paragraphType"))
                                        {
                                            _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.openParagraph;
                                            _paragraphType = (HarvardPlmNews.paragraphType)_sectionType.Items[b];
                                            _markupElementWithLinksType1 = (HarvardPlmNews.MarkupElementWithLinksType)(HarvardPlmNews.paragraphType)_paragraphType;

                                            if (_paragraphType.Items != null)
                                            {
                                                for (int esp = 0; esp < _paragraphType.Items.Length; esp++)
                                                {
                                                    if (_paragraphType.ItemsElementName[esp].ToString().Equals("b"))
                                                    {
                                                        _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.openBold;
                                                        try
                                                        {
                                                            _inlineType1 = (HarvardPlmNews.inlineType)_markupElementWithLinksType1.Items[esp];
                                                            _markupElementWithLinksType2 = (HarvardPlmNews.MarkupElementWithLinksType)_inlineType1;
                                                            _result = _result + _markupElementWithLinksType2.Text[0];
                                                        }
                                                        catch (Exception)
                                                        {
                                                        }
                                                        if (_paragraphType.Text != null)
                                                        {
                                                            foreach (string it in _paragraphType.Text)
                                                            {
                                                                _result = _result + it;
                                                            }
                                                        }
                                                        _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeBold;
                                                    }
                                                    if (_paragraphType.ItemsElementName[esp].ToString().Equals("i"))
                                                    {
                                                        _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.openItalic;

                                                        try
                                                        {
                                                            _inlineType1 = (HarvardPlmNews.inlineType)_markupElementWithLinksType1.Items[esp];
                                                            _markupElementWithLinksType2 = (HarvardPlmNews.MarkupElementWithLinksType)_inlineType1;
                                                            _result = _result + _markupElementWithLinksType2.Text[0];
                                                        }
                                                        catch (Exception)
                                                        {
                                                        }
                                                        if (_paragraphType.Text != null)
                                                        {
                                                            foreach (string it in _paragraphType.Text)
                                                            {
                                                                _result = _result + it;
                                                            }
                                                        }
                                                        _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeItalic;
                                                    }
                                                    if (_paragraphType.ItemsElementName[esp].ToString().Equals("br"))
                                                    {
                                                        _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.breaks;

                                                        try
                                                        {
                                                            _inlineType1 = (HarvardPlmNews.inlineType)_markupElementWithLinksType1.Items[esp];
                                                            _markupElementWithLinksType2 = (HarvardPlmNews.MarkupElementWithLinksType)_inlineType1;
                                                            _result = _result + _markupElementWithLinksType2.Text[0];
                                                        }
                                                        catch (Exception)
                                                        {
                                                        }
                                                        if (_paragraphType.Text != null)
                                                        {
                                                            foreach (string it in _paragraphType.Text)
                                                            {
                                                                _result = _result + it;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (_paragraphType.Text != null)
                                                {
                                                    foreach (string txt in _markupElementWithLinksType1.Text)
                                                    {
                                                        _result = _result + txt;
                                                    }
                                                }
                                            }
                                            _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeParagraph;
                                        }
                                    }
                                }
                                _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeSection;
                            }
                            _result = _result + HarvardContentBusinessEntries.HtmlInfo.Instance.closeTable;
                        }
                        News.content = _result;
                        NewsList.Add(News);
                        _result = "";
                        News = new HarvardContentBusinessEntries.HarvardPlmNewsInfo();

                        //Add the Tracking
                        this.addTracking(code, title);

                    }
                }

                return NewsList;
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }

        }

        public List<String> getTitlesNewsByDate(string code, DateTime date)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                _searchCriteriaType = new HarvardPlmNews.SearchCriteriaType();
                List<string> newsTitles = new List<string>();
                _searchCriteriaType.IncludeBlocked = true;
                _searchCriteriaType.IncludeOriginalDocuments = true;
                _languageType.code = "en";
                _searchCriteriaType.Language = _languageType;
                _searchCriteriaType.PostingDateSpecified = true;
                _searchCriteriaType.PostingDate = DateTime.Parse("01/" + date.Month + "/" + date.Year);

                _documentListType = _documentSearchBinding.DocumentSearch(_searchCriteriaType);

                if (_documentListType.Document != null)
                {
                    foreach (HarvardPlmNews.DocumentListTypeDocument doc in _documentListType.Document)
                    {
                        newsTitles.Add(doc.RegularTitle.Text[0]);
                    }
                }
                return newsTitles;
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<String> getTitlesNewsByTitle(string code, String title)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                _searchCriteriaType = new HarvardPlmNews.SearchCriteriaType();
                List<String> newsTitles = new List<string>();
                _titleType.Value = title;
                _searchCriteriaType.IncludeBlocked = true;
                _searchCriteriaType.IncludeOriginalDocuments = true;
                _searchCriteriaType.Title = _titleType;
                _languageType.code = "en";
                _searchCriteriaType.Language = _languageType;
                _documentListType = _documentSearchBinding.DocumentSearch(_searchCriteriaType);
                if (_documentListType.Document != null)
                {
                    foreach (HarvardPlmNews.DocumentListTypeDocument doc in _documentListType.Document)
                    {
                        newsTitles.Add(doc.RegularTitle.Text[0]);
                    }
                }
                return newsTitles;
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<String> getTitlesNewsByInitialLetter(string code, Char letter)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                _searchCriteriaType = new HarvardPlmNews.SearchCriteriaType();
                List<String> newsTitles = new List<string>();
                _byLetterType.Value = letter.ToString();
                _searchCriteriaType.IncludeBlocked = true;
                _searchCriteriaType.IncludeOriginalDocuments = true;
                _languageType.code = "en";
                _searchCriteriaType.Language = _languageType;
                _searchCriteriaType.ByLetter = _byLetterType;
                _documentListType = _documentSearchBinding.DocumentSearch(_searchCriteriaType);
                if (_documentListType.Document != null)
                {
                    foreach (HarvardPlmNews.DocumentListTypeDocument doc in _documentListType.Document)
                    {
                        newsTitles.Add(doc.RegularTitle.Text[0]);
                    }
                }
                return newsTitles;
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<String> getTitlesNewsByAge(string code, List<string> ages)
        {

            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                _searchCriteriaType = new HarvardPlmNews.SearchCriteriaType();
                List<String> newsTitles = new List<string>();
                HarvardPlmNews.AgeGroupType[] AgeGroupType_ = new HarvardPlmNews.AgeGroupType[ages.Count];
                int count = 0;
                foreach (string Ag in ages)
                {
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Infant.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Infant011mo;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Child.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Childhood11mo12yrs;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Teen.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Teen1218yrs;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Adult.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Adult18;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.GrownUP))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Senior;
                    }
                }

                _searchCriteriaType.IncludeBlocked = true;
                _searchCriteriaType.IncludeOriginalDocuments = true;
                _languageType.code = "en";
                _searchCriteriaType.Language = _languageType;
                _searchCriteriaType.AgeGroups = AgeGroupType_;
                _documentListType = _documentSearchBinding.DocumentSearch(_searchCriteriaType);
                if (_documentListType.Document != null)
                {
                    foreach (HarvardPlmNews.DocumentListTypeDocument doc in _documentListType.Document)
                    {
                        newsTitles.Add(doc.RegularTitle.Text[0]);
                    }
                }
                return newsTitles;
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<String> getTitlesNewsByInitialLetterByDateByTitleByAge(string code, Char letter, DateTime date, String title, List<string> ages)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                _searchCriteriaType = new HarvardPlmNews.SearchCriteriaType();
                List<String> newsTitles = new List<string>();
                _byLetterType.Value = letter.ToString();
                _searchCriteriaType.IncludeBlocked = true;
                _searchCriteriaType.IncludeOriginalDocuments = true;
                _languageType.code = "en";
                _searchCriteriaType.Language = _languageType;
                _searchCriteriaType.ByLetter = _byLetterType;
                _searchCriteriaType.PostingDateSpecified = true;
                _searchCriteriaType.PostingDate = date;
                _titleType.Value = title;
                _searchCriteriaType.Title = _titleType;
                HarvardPlmNews.AgeGroupType[] AgeGroupType_ = new HarvardPlmNews.AgeGroupType[ages.Count];
                int count = 0;
                foreach (string Ag in ages)
                {
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Infant.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Infant011mo;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Child.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Childhood11mo12yrs;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Teen.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Teen1218yrs;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Adult.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Adult18;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.GrownUP))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Senior;
                    }
                }
                _searchCriteriaType.AgeGroups = AgeGroupType_;
                _documentListType = _documentSearchBinding.DocumentSearch(_searchCriteriaType);
                if (_documentListType.Document != null)
                {
                    foreach (HarvardPlmNews.DocumentListTypeDocument doc in _documentListType.Document)
                    {

                        newsTitles.Add(doc.RegularTitle.Text[0]);

                    }
                }
                return newsTitles;
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<String> getTitlesNewsByInitialLetterByDateByTitle(string code, Char letter, DateTime date, String title)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                _searchCriteriaType = new HarvardPlmNews.SearchCriteriaType();
                List<String> newsTitles = new List<string>();
                _byLetterType.Value = letter.ToString();
                _searchCriteriaType.IncludeBlocked = true;
                _searchCriteriaType.IncludeOriginalDocuments = true;
                _languageType.code = "en";
                _searchCriteriaType.Language = _languageType;
                _searchCriteriaType.ByLetter = _byLetterType;
                _searchCriteriaType.PostingDateSpecified = true;
                _searchCriteriaType.PostingDate = date;
                _titleType.Value = title;
                _searchCriteriaType.Title = _titleType;
                _documentListType = _documentSearchBinding.DocumentSearch(_searchCriteriaType);
                if (_documentListType.Document != null)
                {
                    foreach (HarvardPlmNews.DocumentListTypeDocument doc in _documentListType.Document)
                    {

                        newsTitles.Add(doc.RegularTitle.Text[0]);
                    }
                }
                return newsTitles;
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<String> getTitlesNewsByInitialLetterByTitleByAge(string code, Char letter, String title, List<string> ages)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                _searchCriteriaType = new HarvardPlmNews.SearchCriteriaType();
                List<String> newsTitles = new List<string>();
                _byLetterType.Value = letter.ToString();

                _searchCriteriaType.IncludeBlocked = true;
                _searchCriteriaType.IncludeOriginalDocuments = true;
                _languageType.code = "en";
                _searchCriteriaType.Language = _languageType;
                _searchCriteriaType.ByLetter = _byLetterType;
                _titleType.Value = title;
                _searchCriteriaType.Title = _titleType;
                HarvardPlmNews.AgeGroupType[] AgeGroupType_ = new HarvardPlmNews.AgeGroupType[ages.Count];
                int count = 0;
                foreach (string Ag in ages)
                {
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Infant.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Infant011mo;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Child.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Childhood11mo12yrs;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Teen.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Teen1218yrs;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Adult.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Adult18;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.GrownUP))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Senior;
                    }
                }
                _searchCriteriaType.AgeGroups = AgeGroupType_;
                _documentListType = _documentSearchBinding.DocumentSearch(_searchCriteriaType);
                if (_documentListType.Document != null)
                {
                    foreach (HarvardPlmNews.DocumentListTypeDocument doc in _documentListType.Document)
                    {
                        newsTitles.Add(doc.RegularTitle.Text[0]);
                    }
                }
                return newsTitles;
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<String> getTitlesNewsByInitialLetterByDateByAge(string code, Char letter, DateTime date, List<string> ages)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                _searchCriteriaType = new HarvardPlmNews.SearchCriteriaType();
                List<String> newsTitles = new List<string>();
                _byLetterType.Value = letter.ToString();
                _searchCriteriaType.IncludeBlocked = true;
                _searchCriteriaType.IncludeOriginalDocuments = true;
                _languageType.code = "en";
                _searchCriteriaType.Language = _languageType;
                _searchCriteriaType.ByLetter = _byLetterType;
                _searchCriteriaType.PostingDateSpecified = true;
                _searchCriteriaType.PostingDate = date;
                HarvardPlmNews.AgeGroupType[] AgeGroupType_ = new HarvardPlmNews.AgeGroupType[ages.Count];
                int count = 0;
                foreach (string Ag in ages)
                {
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Infant.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Infant011mo;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Child.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Childhood11mo12yrs;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Teen.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Teen1218yrs;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Adult.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Adult18;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.GrownUP))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Senior;
                    }
                }
                _searchCriteriaType.AgeGroups = AgeGroupType_;
                _documentListType = _documentSearchBinding.DocumentSearch(_searchCriteriaType);
                if (_documentListType.Document != null)
                {
                    foreach (HarvardPlmNews.DocumentListTypeDocument doc in _documentListType.Document)
                    {
                        newsTitles.Add(doc.RegularTitle.Text[0]);
                    }
                }
                return newsTitles;
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<String> getTitlesNewsByDateByTitleByAge(string code, DateTime date, String title, List<string> ages)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                _searchCriteriaType = new HarvardPlmNews.SearchCriteriaType();
                List<String> newsTitles = new List<string>();
                _searchCriteriaType.IncludeBlocked = true;
                _searchCriteriaType.IncludeOriginalDocuments = true;
                _languageType.code = "en";
                _searchCriteriaType.Language = _languageType;
                _searchCriteriaType.PostingDateSpecified = true;
                _searchCriteriaType.PostingDate = date;
                _titleType.Value = title;
                _searchCriteriaType.Title = _titleType;
                HarvardPlmNews.AgeGroupType[] AgeGroupType_ = new HarvardPlmNews.AgeGroupType[ages.Count];
                int count = 0;
                foreach (string Ag in ages)
                {
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Infant.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Infant011mo;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Child.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Childhood11mo12yrs;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Teen.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Teen1218yrs;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Adult.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Adult18;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.GrownUP))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Senior;
                    }
                }
                _searchCriteriaType.AgeGroups = AgeGroupType_;
                _documentListType = _documentSearchBinding.DocumentSearch(_searchCriteriaType);
                if (_documentListType.Document != null)
                {
                    foreach (HarvardPlmNews.DocumentListTypeDocument doc in _documentListType.Document)
                    {
                        newsTitles.Add(doc.RegularTitle.Text[0]);
                    }
                }
                return newsTitles;
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<String> getTitlesNewsByInitialLetterByTitle(string code, Char letter, String title)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                _searchCriteriaType = new HarvardPlmNews.SearchCriteriaType();
                List<String> newsTitles = new List<string>();
                _byLetterType.Value = letter.ToString();
                _searchCriteriaType.IncludeBlocked = true;
                _searchCriteriaType.IncludeOriginalDocuments = true;
                _languageType.code = "en";
                _searchCriteriaType.Language = _languageType;
                _searchCriteriaType.ByLetter = _byLetterType;
                _titleType.Value = title;
                _searchCriteriaType.Title = _titleType;
                _documentListType = _documentSearchBinding.DocumentSearch(_searchCriteriaType);
                if (_documentListType.Document != null)
                {
                    foreach (HarvardPlmNews.DocumentListTypeDocument doc in _documentListType.Document)
                    {
                        newsTitles.Add(doc.RegularTitle.Text[0]);
                    }
                }
                return newsTitles;
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<String> getTitlesNewsByInitialLetterByAge(string code, Char letter, List<string> ages)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                _searchCriteriaType = new HarvardPlmNews.SearchCriteriaType();
                List<String> newsTitles = new List<string>();
                _byLetterType.Value = letter.ToString();
                _searchCriteriaType.IncludeBlocked = true;
                _searchCriteriaType.IncludeOriginalDocuments = true;
                _languageType.code = "en";
                _searchCriteriaType.Language = _languageType;
                _searchCriteriaType.ByLetter = _byLetterType;
                HarvardPlmNews.AgeGroupType[] AgeGroupType_ = new HarvardPlmNews.AgeGroupType[ages.Count];
                int count = 0;
                foreach (string Ag in ages)
                {
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Infant.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Infant011mo;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Child.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Childhood11mo12yrs;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Teen.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Teen1218yrs;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Adult.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Adult18;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.GrownUP))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Senior;
                    }
                }
                _searchCriteriaType.AgeGroups = AgeGroupType_;
                _documentListType = _documentSearchBinding.DocumentSearch(_searchCriteriaType);
                if (_documentListType.Document != null)
                {
                    foreach (HarvardPlmNews.DocumentListTypeDocument doc in _documentListType.Document)
                    {
                        newsTitles.Add(doc.RegularTitle.Text[0]);
                    }
                }
                return newsTitles;
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<String> getTitlesNewsByInitialLetterByDate(string code, Char letter, DateTime date)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                _searchCriteriaType = new HarvardPlmNews.SearchCriteriaType();
                _byLetterType.Value = letter.ToString();
                List<String> newsTitles = new List<string>();

                _searchCriteriaType.IncludeBlocked = true;
                _searchCriteriaType.IncludeOriginalDocuments = true;
                _languageType.code = "en";
                _searchCriteriaType.Language = _languageType;
                _searchCriteriaType.ByLetter = _byLetterType;
                _searchCriteriaType.PostingDateSpecified = true;
                _searchCriteriaType.PostingDate = date;
                _documentListType = _documentSearchBinding.DocumentSearch(_searchCriteriaType);
                if (_documentListType.Document != null)
                {
                    foreach (HarvardPlmNews.DocumentListTypeDocument doc in _documentListType.Document)
                    {

                        newsTitles.Add(doc.RegularTitle.Text[0]);

                    }
                }
                return newsTitles;
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<String> getTitlesNewsByTitleByAge(string code, String title, List<string> ages)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                _searchCriteriaType = new HarvardPlmNews.SearchCriteriaType();
                List<String> newsTitles = new List<string>();
                _searchCriteriaType.IncludeBlocked = true;
                _searchCriteriaType.IncludeOriginalDocuments = true;
                _languageType.code = "en";
                _searchCriteriaType.Language = _languageType;
                _titleType.Value = title;
                _searchCriteriaType.Title = _titleType;
                HarvardPlmNews.AgeGroupType[] AgeGroupType_ = new HarvardPlmNews.AgeGroupType[ages.Count];
                int count = 0;
                foreach (string Ag in ages)
                {
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Infant.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Infant011mo;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Child.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Childhood11mo12yrs;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Teen.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Teen1218yrs;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Adult.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Adult18;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.GrownUP))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Senior;
                    }
                }
                _searchCriteriaType.AgeGroups = AgeGroupType_;
                _documentListType = _documentSearchBinding.DocumentSearch(_searchCriteriaType);
                if (_documentListType.Document != null)
                {
                    foreach (HarvardPlmNews.DocumentListTypeDocument doc in _documentListType.Document)
                    {

                        newsTitles.Add(_documentType.RegularTitle.Text[0]);

                    }
                }
                return newsTitles;
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<String> getTitlesNewsByDateByTitle(string code, DateTime date, String title)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                _searchCriteriaType = new HarvardPlmNews.SearchCriteriaType();
                List<String> newsTitles = new List<string>();

                _searchCriteriaType.IncludeBlocked = true;
                _searchCriteriaType.IncludeOriginalDocuments = true;
                _languageType.code = "en";
                _searchCriteriaType.Language = _languageType;
                _searchCriteriaType.PostingDateSpecified = true;
                _searchCriteriaType.PostingDate = date;
                _titleType.Value = title;
                _searchCriteriaType.Title = _titleType;
                _documentListType = _documentSearchBinding.DocumentSearch(_searchCriteriaType);
                if (_documentListType.Document != null)
                {
                    foreach (HarvardPlmNews.DocumentListTypeDocument doc in _documentListType.Document)
                    {

                        newsTitles.Add(_documentType.RegularTitle.Text[0]);

                    }
                }
                return newsTitles;
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        public List<String> getTitlesNewsByDateByAge(string code, DateTime date, List<string> ages)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);
            if (validCode != null && validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                _searchCriteriaType = new HarvardPlmNews.SearchCriteriaType();
                List<String> newsTitles = new List<string>();
                _searchCriteriaType.IncludeBlocked = true;
                _searchCriteriaType.IncludeOriginalDocuments = true;
                _languageType.code = "en";
                _searchCriteriaType.Language = _languageType;
                _searchCriteriaType.PostingDateSpecified = true;
                _searchCriteriaType.PostingDate = date;
                HarvardPlmNews.AgeGroupType[] AgeGroupType_ = new HarvardPlmNews.AgeGroupType[ages.Count];
                int count = 0;
                foreach (string Ag in ages)
                {
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Infant.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Infant011mo;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Child.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Childhood11mo12yrs;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Teen.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Teen1218yrs;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.Adult.ToString()))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Adult18;
                    }
                    if (Ag.Equals(HarvardContentBusinessEntries.NumberingInfo.Ages.GrownUP))
                    {
                        AgeGroupType_[count] = HarvardPlmNews.AgeGroupType.Senior;
                    }
                }
                _searchCriteriaType.AgeGroups = AgeGroupType_;
                _documentListType = _documentSearchBinding.DocumentSearch(_searchCriteriaType);
                if (_documentListType.Document != null)
                {
                    foreach (HarvardPlmNews.DocumentListTypeDocument doc in _documentListType.Document)
                    {
                        newsTitles.Add(_documentType.RegularTitle.Text[0]);

                    }
                }
                return newsTitles;
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }
        
        #endregion
        
        #region Objects WS
        
        private HarvardPlmNews.paragraphType _paragraphType = new HarvardPlmNews.paragraphType();
        private HarvardPlmNews.DocumentSearchBinding _documentSearchBinding = new HarvardPlmNews.DocumentSearchBinding();
        private HarvardPlmNews.GetDocumentBinding _getDocumentBinding = new HarvardPlmNews.GetDocumentBinding();
        private HarvardPlmNews.DocumentRequest _documentRequest = new HarvardPlmNews.DocumentRequest();
        private HarvardPlmNews.DocumentType _documentType = new HarvardPlmNews.DocumentType();
        private HarvardPlmNews.DocumentListType _documentListType = new HarvardPlmNews.DocumentListType();
        private HarvardPlmNews.DocumentTypeContent _documentTypeContent = new HarvardPlmNews.DocumentTypeContent();
        private HarvardPlmNews.SearchCriteriaType _searchCriteriaType = new HarvardPlmNews.SearchCriteriaType();
        private HarvardPlmNews.ByLetterType _byLetterType = new HarvardPlmNews.ByLetterType();
        private HarvardPlmNews.TitleType _titleType = new HarvardPlmNews.TitleType();
        private HarvardPlmNews.LanguageType _languageType = new HarvardPlmNews.LanguageType();
        private HarvardPlmNews.bodyType _bodyType = new HarvardPlmNews.bodyType();
        private HarvardPlmNews.SectionType _sectionType = new HarvardPlmNews.SectionType();
        private HarvardPlmNews.SectionType _sectionTypeSub = new HarvardPlmNews.SectionType();
        private HarvardPlmNews.SectionType _sectionTypeSub_Sub = new HarvardPlmNews.SectionType();
        private HarvardPlmNews.MarkupElementWithLinksType _markupElementWithLinksType1 = new HarvardPlmNews.MarkupElementWithLinksType();
        private HarvardPlmNews.MarkupElementWithLinksType _markupElementWithLinksType2 = new HarvardPlmNews.MarkupElementWithLinksType();
        private HarvardPlmNews.inlineType _inlineType1 = new HarvardPlmNews.inlineType();
        private HarvardPlmNews.MarkupElementWithLinksType _markupElementWithLinksType3 = new HarvardPlmNews.MarkupElementWithLinksType();
        private HarvardPlmNews.inlineType _inlineType2 = new HarvardPlmNews.inlineType();
        private HarvardPlmNews.listULType _listULType = new HarvardPlmNews.listULType();
        private HarvardPlmNews.HTMLBlockType _htmlBlockType = new HarvardPlmNews.HTMLBlockType();
        private HarvardPlmNews.tableType _tableType = new HarvardPlmNews.tableType();
        private HarvardPlmNews.tableSectionType _tblSectionType = new HarvardPlmNews.tableSectionType();
        private HarvardPlmNews.trType _trType = new HarvardPlmNews.trType();
        private HarvardPlmNews.tdType _tdType = new HarvardPlmNews.tdType();
        
        #endregion

        #region Private Methods

        private void addTracking(string code, string title)
        {
            InfoLogsBusinessEntries.Info_TrackingListInfo trackingInfo = new InfoLogsBusinessEntries.Info_TrackingListInfo();
            trackingInfo.ParentId = null;
            trackingInfo.CodeString = code;
            trackingInfo.SearchDate = null;
            trackingInfo.SourceId = (byte)InfoLogsBusinessEntries.Catalogs.Info_Sources.Servicio_Web;
            trackingInfo.SearchTypeId = (byte)InfoLogsBusinessEntries.Catalogs.Info_SearchTypes.Texto;
            trackingInfo.EntityId = (int)InfoLogsBusinessEntries.Catalogs.Info_Entities.Noticias_Harvard;
            trackingInfo.SearchParameters = "Noticia=" + title;
            trackingInfo.JSONFormat = null;
            trackingInfo.Replicate = false;

            this._PLMLogs.addTrackingActivity(trackingInfo);
        }

        #endregion

        #region Private Instances

        private WCFPLMLogsServices.PLMLogs _PLMLogs = new WCFPLMLogsServices.PLMLogs();

        #endregion

        string _result = "";
        
        public static readonly HarvardPmlBLC Instance = new HarvardPmlBLC();
    }
}
