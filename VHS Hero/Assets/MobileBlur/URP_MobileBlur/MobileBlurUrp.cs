namespace UnityEngine.Rendering.Universal
{
    public class MobileBlurUrp : ScriptableRendererFeature
    {
        [System.Serializable]
        public class MobileBlurSettings
        {
            public RenderPassEvent Event = RenderPassEvent.AfterRenderingTransparents;

            [Range(1, 5)]
            public int NumberOfPasses = 3;

            [Range(2, 3)]
            public int KernelSize = 2;

            [Range(0, 3)]
            public float BlurAmount = 1f;

            public Texture2D BlurMask;

            public Material BlitMaterial = null;
        }

        public MobileBlurSettings settings = new MobileBlurSettings();

        MobileBlurUrpPass mobileBlurLwrpPass;

        public override void Create()
        {
            mobileBlurLwrpPass = new MobileBlurUrpPass(settings.Event, settings.BlitMaterial, settings.NumberOfPasses, settings.KernelSize, settings.BlurAmount, settings.BlurMask, this.name);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            mobileBlurLwrpPass.Setup(renderer.cameraColorTarget);
            renderer.EnqueuePass(mobileBlurLwrpPass);
        }
    }
}

