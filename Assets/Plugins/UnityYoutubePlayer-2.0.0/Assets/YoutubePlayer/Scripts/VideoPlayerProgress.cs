using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

namespace YoutubePlayer
{
    /// <summary>
    /// A progress bar for VideoPlayer
    /// </summary>
    [RequireComponent(typeof(Image), typeof(RectTransform))]
    public class VideoPlayerProgress : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
    {
        /// <summary>
        /// Is seeking through the video enabled?
        /// </summary>
        public bool SeekingEnabled, checkTemp;

        /// <summary>
        /// The VideoPlayer to synchronize with
        /// </summary>
        public VideoPlayer videoPlayer;

        Image m_PlaybackProgress;
        RectTransform m_RectTransform;

        void Start()
        {
            m_RectTransform = GetComponent<RectTransform>();
            m_PlaybackProgress = GetComponent<Image>();

            if (m_PlaybackProgress.sprite == null)
            {
                var texture = Texture2D.whiteTexture;
                var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 100);
                m_PlaybackProgress.sprite = sprite;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (videoPlayer.isPlaying)
            {
                m_PlaybackProgress.fillAmount =
                    (float)(videoPlayer.length > 0 ? videoPlayer.time / videoPlayer.length : 0);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Seek(Input.mousePosition);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Seek(Input.mousePosition);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            checkTemp = videoPlayer.isPlaying;
            videoPlayer.Pause();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (checkTemp)
                videoPlayer.Play();

            checkTemp = false;
        }

        void Seek(Vector2 cursorPosition)
        {
            if (!SeekingEnabled || !videoPlayer.canSetTime)
                return;

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                m_RectTransform, cursorPosition, null, out var localPoint))
                return;

            var rect = m_RectTransform.rect;
            var progress = (localPoint.x - rect.x)  / rect.width;

            videoPlayer.time = videoPlayer.length * progress;
            m_PlaybackProgress.fillAmount = progress;
        }
    }
}
