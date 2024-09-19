using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UI.Pagination
{
    public partial class PagedRect
    {
        /// <summary>
        /// Updates the page buttons
        /// </summary>
        public void UpdatePagination()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                // If we're not active and in the game, don't do this
                if (!this.gameObject.activeInHierarchy)
                {
                    return;
                }

                // If we're not dirty, then don't do this
                if (!this.isDirty)
                {
                    return;
                }
            }
#endif
            var paginationButtons = GetComponentsInChildren<PaginationButton>(true)
                                    .Where(pb => !pb.DontUpdate)    // don't select buttons that we shouldn't be adjusting (e.g. templates, first/last, next/previous)
                                    .Where(pb => pb != ButtonTemplate_CurrentPage && pb != ButtonTemplate_OtherPages) // just in case we accidentally clear the DontUpdate field on the templates
                                    .Where(pb => pb.transform.parent == Pagination.transform)
                                    .ToList();

            paginationButtons.ForEach(pb =>
            {
                if (Application.isPlaying)
                {
                    Destroy(pb.gameObject);
                }
                else
                {
                    // Destroy doesn't work in edit mode
                    DestroyImmediate(pb.Text.gameObject);
                    DestroyImmediate(pb.gameObject);
                }
            });

            if (!ShowPagination || !ShowPageButtons)
            {
                return;
            }

            var startingPoint = 0;
            var endingPoint = NumberOfPages - 1;

            if (MaximumNumberOfButtonsToShow != 0)
            {
                if (MaximumNumberOfButtonsToShow == 1)
                {
                    startingPoint = CurrentPage - 1;
                    endingPoint = CurrentPage - 1;
                }
                else if (NumberOfPages > MaximumNumberOfButtonsToShow)
                {
                    var halfOfMax = (int)Math.Floor(MaximumNumberOfButtonsToShow / 2f);

                    var distanceFromStart = CurrentPage - startingPoint;
                    var distanceFromEnd = endingPoint - CurrentPage;

                    if (distanceFromStart <= distanceFromEnd)
                    {
                        if (CurrentPage > halfOfMax)
                        {
                            startingPoint = CurrentPage - halfOfMax;
                        }

                        endingPoint = Math.Min(endingPoint, startingPoint + MaximumNumberOfButtonsToShow - 1);
                    }
                    else
                    {
                        if (distanceFromEnd > halfOfMax)
                        {
                            endingPoint = CurrentPage + halfOfMax;
                        }

                        startingPoint = Math.Max(startingPoint, endingPoint - MaximumNumberOfButtonsToShow + 1);
                    }
                }
            }
            

            for (int i = startingPoint; i <= endingPoint; i++)
            {
                var pageNumber = i + 1;
                //var page = Pages[i];

                Page page = GetPageByNumber(pageNumber, false, true);                
                                
                if (page == null)
                {                    
                    // no page was found by this number
                    // this means a page was most likely deleted
                    // renumber pages and start again
                    PageWasDeleted(pageNumber);
                    
                    continue;
                }                

                var template = (pageNumber == CurrentPage) ? ButtonTemplate_CurrentPage : ButtonTemplate_OtherPages;
                if (!page.PageEnabled && ButtonTemplate_DisabledPage != null)
                {
                    template = ButtonTemplate_DisabledPage;
                }

                var button = Instantiate(template) as PaginationButton;

                if (page.PageEnabled)
                {
                    // Add the onClick listener
                    button.Button.onClick.AddListener(new UnityEngine.Events.UnityAction(() => { this.SetCurrentPage((pageNumber)); }));
                }

                // Position the button
                button.gameObject.transform.SetParent(Pagination.transform, false);

                var buttonText = "";
                if (ShowNumbersOnButtons)
                {
                    buttonText = pageNumber.ToString();
                    if (ShowPageTitlesOnButtons)
                    {
                        buttonText += ". ";
                    }
                }

                if (ShowPageTitlesOnButtons)
                {
                    buttonText += page.PageTitle;
                }

                button.SetText(buttonText);

                button.gameObject.name = String.Format("Button - Page {0} {1}", pageNumber, pageNumber == CurrentPage ? "(Current Page)" : "");

                // DO update this button
                button.DontUpdate = false;

                // Activate the button if need be (the template is usually disabled at this point)
                button.gameObject.SetActive(true);
            }

            // ensure our other buttons are in the right places
            Button_PreviousPage.gameObject.transform.SetAsFirstSibling();
            Button_FirstPage.gameObject.transform.SetAsFirstSibling();
            Button_NextPage.gameObject.transform.SetAsLastSibling();
            Button_LastPage.gameObject.transform.SetAsLastSibling();
        }

        void ToggleTemplateButtons(bool show)
        {
            if (ButtonTemplate_CurrentPage != null) ButtonTemplate_CurrentPage.gameObject.SetActive(show);
            if (ButtonTemplate_OtherPages != null) ButtonTemplate_OtherPages.gameObject.SetActive(show);
            if (ButtonTemplate_DisabledPage != null) ButtonTemplate_DisabledPage.gameObject.SetActive(show);
        }

        void ToggleFirstAndLastButtons(bool show)
        {
            if (Button_FirstPage != null) Button_FirstPage.gameObject.SetActive(show);
            if (Button_LastPage != null) Button_LastPage.gameObject.SetActive(show);
        }

        void TogglePreviousAndNextButtons(bool show)
        {
            if (Button_NextPage != null) Button_NextPage.gameObject.SetActive(show);
            if (Button_PreviousPage != null) Button_PreviousPage.gameObject.SetActive(show);
        }
    }
}
