using UnityEngine;

public class OnlineMapsGUITooltipDrawer: OnlineMapsTooltipDrawerBase
{
    /// <summary>
    /// Allows you to customize the appearance of the tooltip.
    /// </summary>
    /// <param name="style">The reference to the style.</param>
    public delegate void OnPrepareTooltipStyleDelegate(ref GUIStyle style);

    /// <summary>
    /// Event caused when preparing tooltip style.
    /// </summary>
    public static OnPrepareTooltipStyleDelegate OnPrepareTooltipStyle;

    private GUIStyle tooltipStyle;

    public OnlineMapsGUITooltipDrawer(OnlineMaps map)
    {
        this.map = map;
        control = map.control;
        map.OnGUIAfter += DrawTooltips;

        tooltipStyle = new GUIStyle
        {
            normal =
            {
                background = map.tooltipBackgroundTexture,
                textColor = new Color32(230, 230, 230, 255)
            },
            border = new RectOffset(8, 8, 8, 8),
            margin = new RectOffset(4, 4, 4, 4),
            wordWrap = true,
            richText = true,
            alignment = TextAnchor.MiddleCenter,
            stretchWidth = true,
            padding = new RectOffset(0, 0, 3, 3)
        };
    }

    ~OnlineMapsGUITooltipDrawer()
    {
        map.OnGUIAfter -= DrawTooltips;
        OnPrepareTooltipStyle = null;
        tooltipStyle = null;
        map = null;
    }

    private void OnGUITooltip(GUIStyle style, string text, Vector2 position)
    {
        GUIContent tip = new GUIContent(text);
        Vector2 size = style.CalcSize(tip);
        GUI.Label(new Rect(position.x - size.x / 2 - 5, Screen.height - position.y - size.y - 20, size.x + 10, size.y + 5), text, style);
    }

    private void DrawTooltips()
    {
        if (string.IsNullOrEmpty(tooltip) && map.showMarkerTooltip != OnlineMapsShowMarkerTooltip.always) return;

        GUIStyle style = new GUIStyle(tooltipStyle);

        if (OnPrepareTooltipStyle != null) OnPrepareTooltipStyle(ref style);

        if (!string.IsNullOrEmpty(tooltip))
        {
            Vector2 inputPosition = control.GetInputPosition();

            if (tooltipMarker != null)
            {
                if (tooltipMarker.OnDrawTooltip != null) tooltipMarker.OnDrawTooltip(tooltipMarker);
                else if (OnlineMapsMarkerBase.OnMarkerDrawTooltip != null) OnlineMapsMarkerBase.OnMarkerDrawTooltip(tooltipMarker);
                else OnGUITooltip(style, tooltip, inputPosition);
            }
            else if (tooltipDrawingElement != null)
            {
                if (tooltipDrawingElement.OnDrawTooltip != null) tooltipDrawingElement.OnDrawTooltip(tooltipDrawingElement);
                else if (OnlineMapsDrawingElement.OnElementDrawTooltip != null) OnlineMapsDrawingElement.OnElementDrawTooltip(tooltipDrawingElement);
                else OnGUITooltip(style, tooltip, inputPosition);
            }
        }

        if (map.showMarkerTooltip == OnlineMapsShowMarkerTooltip.always)
        {
            if (OnlineMapsControlBase.instance is OnlineMapsTileSetControl)
            {
                OnlineMapsTileSetControl tsControl = OnlineMapsTileSetControl.instance;

                double tlx, tly, brx, bry;
                map.GetCorners(out tlx, out tly, out brx, out bry);
                if (brx < tlx) brx += 360;

                foreach (OnlineMapsMarker marker in OnlineMapsMarkerManager.instance)
                {
                    if (string.IsNullOrEmpty(marker.label)) continue;

                    double mx, my;
                    marker.GetPosition(out mx, out my);

                    if (!(((mx > tlx && mx < brx) || (mx + 360 > tlx && mx + 360 < brx) || (mx - 360 > tlx && mx - 360 < brx)) && my < tly && my > bry)) continue;

                    if (marker.OnDrawTooltip != null) marker.OnDrawTooltip(marker);
                    else if (OnlineMapsMarkerBase.OnMarkerDrawTooltip != null) OnlineMapsMarkerBase.OnMarkerDrawTooltip(marker);
                    else
                    {
                        Vector3 p1 = tsControl.GetWorldPositionWithElevation(mx, my, tlx, tly, brx, bry);
                        Vector3 p2 = p1 + new Vector3(0, 0, tsControl.sizeInScene.y / map.height * marker.height * marker.scale);

                        Vector2 screenPoint1 = tsControl.activeCamera.WorldToScreenPoint(p1);
                        Vector2 screenPoint2 = tsControl.activeCamera.WorldToScreenPoint(p2);

                        float yOffset = (screenPoint1.y - screenPoint2.y) * map.transform.localScale.x - 10;

                        OnGUITooltip(style, marker.label, screenPoint1 + new Vector2(0, yOffset));
                    }
                }

                foreach (OnlineMapsMarker3D marker in OnlineMapsMarker3DManager.instance)
                {
                    if (string.IsNullOrEmpty(marker.label)) continue;

                    double mx, my;
                    marker.GetPosition(out mx, out my);

                    if (!(((mx > tlx && mx < brx) || (mx + 360 > tlx && mx + 360 < brx) ||
                           (mx - 360 > tlx && mx - 360 < brx)) &&
                          my < tly && my > bry)) continue;

                    if (marker.OnDrawTooltip != null) marker.OnDrawTooltip(marker);
                    else if (OnlineMapsMarkerBase.OnMarkerDrawTooltip != null) OnlineMapsMarkerBase.OnMarkerDrawTooltip(marker);
                    else
                    {
                        Vector3 p1 = tsControl.GetWorldPositionWithElevation(mx, my, tlx, tly, brx, bry);
                        Vector3 p2 = p1 + new Vector3(0, 0, tsControl.sizeInScene.y / map.height * marker.scale);

                        Vector2 screenPoint1 = tsControl.activeCamera.WorldToScreenPoint(p1);
                        Vector2 screenPoint2 = tsControl.activeCamera.WorldToScreenPoint(p2);

                        float yOffset = (screenPoint1.y - screenPoint2.y) * map.transform.localScale.x - 10;

                        OnGUITooltip(style, marker.label, screenPoint1 + new Vector2(0, yOffset));
                    }
                }
            }
            else
            {
                foreach (OnlineMapsMarker marker in OnlineMapsMarkerManager.instance)
                {
                    if (string.IsNullOrEmpty(marker.label)) continue;

                    Rect rect = marker.screenRect;

                    if (rect.xMax > 0 && rect.xMin < Screen.width && rect.yMax > 0 && rect.yMin < Screen.height)
                    {
                        if (marker.OnDrawTooltip != null) marker.OnDrawTooltip(marker);
                        else if (OnlineMapsMarkerBase.OnMarkerDrawTooltip != null) OnlineMapsMarkerBase.OnMarkerDrawTooltip(marker);
                        else OnGUITooltip(style, marker.label, new Vector2(rect.x + rect.width / 2, rect.y + rect.height));
                    }
                }

                if (map.control is OnlineMapsControlBase3D)
                {
                    double tlx, tly, brx, bry;
                    map.GetCorners(out tlx, out tly, out brx, out bry);
                    if (brx < tlx) brx += 360;

                    foreach (OnlineMapsMarker3D marker in OnlineMapsMarker3DManager.instance)
                    {
                        if (string.IsNullOrEmpty(marker.label)) continue;

                        double mx, my;
                        marker.GetPosition(out mx, out my);

                        if (!(((mx > tlx && mx < brx) || (mx + 360 > tlx && mx + 360 < brx) ||
                               (mx - 360 > tlx && mx - 360 < brx)) &&
                              my < tly && my > bry)) continue;

                        if (marker.OnDrawTooltip != null) marker.OnDrawTooltip(marker);
                        else if (OnlineMapsMarkerBase.OnMarkerDrawTooltip != null) OnlineMapsMarkerBase.OnMarkerDrawTooltip(marker);
                        else
                        {
                            double mx1, my1;
                            OnlineMapsControlBase3D.instance.GetPosition(mx, my, out mx1, out my1);

                            double px = (-mx1 / map.width + 0.5) * OnlineMapsControlBase3D.instance.cl.bounds.size.x;
                            double pz = (my1 / map.height - 0.5) * OnlineMapsControlBase3D.instance.cl.bounds.size.z;

                            Vector3 offset = map.transform.rotation * new Vector3((float)px, 0, (float)pz);
                            offset.Scale(map.transform.lossyScale);

                            Vector3 p1 = map.transform.position + offset;
                            Vector3 p2 = p1 + new Vector3(0, 0, OnlineMapsControlBase3D.instance.cl.bounds.size.z / map.height * marker.scale);

                            Vector2 screenPoint1 = OnlineMapsControlBase3D.instance.activeCamera.WorldToScreenPoint(p1);
                            Vector2 screenPoint2 = OnlineMapsControlBase3D.instance.activeCamera.WorldToScreenPoint(p2);

                            float yOffset = (screenPoint1.y - screenPoint2.y) * map.transform.localScale.x - 10;

                            OnGUITooltip(style, marker.label, screenPoint1 + new Vector2(0, yOffset));
                        }
                    }
                }
            }
        }
    }
}