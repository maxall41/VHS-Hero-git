namespace UnityEngine.Rendering.Universal
{
    internal class MobileBlurUrpPass : ScriptableRenderPass
    {
        public Material material;

        private RenderTargetIdentifier source;
        private RenderTargetIdentifier blurTemp = new RenderTargetIdentifier(blurTempString);
        private RenderTargetIdentifier blurTemp1 = new RenderTargetIdentifier(blurTemp1String);
        private RenderTargetIdentifier blurTex = new RenderTargetIdentifier(blurTexString);
        private RenderTargetIdentifier tempCopy = new RenderTargetIdentifier(tempCopyString);

        private readonly string tag;
        private readonly int numberOfPasses;
        private readonly int kernelSize;
        private readonly float blurAmount;
        private Texture2D blurMask, previous;

        static readonly string kernelKeyword = "KERNEL";
        static readonly int blurAmountString = Shader.PropertyToID("_BlurAmount");
        static readonly int maskTextureString = Shader.PropertyToID("_MaskTex");
        static readonly int blurTexString = Shader.PropertyToID("_BlurTex");
        static readonly int blurTempString = Shader.PropertyToID("_BlurTemp");
        static readonly int blurTemp1String = Shader.PropertyToID("_BlurTemp1");
        static readonly int tempCopyString = Shader.PropertyToID("_TempCopy");

        public MobileBlurUrpPass(RenderPassEvent renderPassEvent, Material material,
            int numberOfPasses, int kernelSize, float blurAmount, Texture2D blurMask, string tag)
        {
            this.renderPassEvent = renderPassEvent;
            this.tag = tag;
            this.material = material;
            this.blurAmount = blurAmount;
            this.kernelSize = kernelSize;
            this.blurMask = blurMask == null ? Texture2D.whiteTexture : blurMask;
            this.numberOfPasses = numberOfPasses;
        }

        public void Setup(RenderTargetIdentifier source)
        {
            this.source = source;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(tag);
            RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
            opaqueDesc.depthBufferBits = 0;

            cmd.GetTemporaryRT(tempCopyString, opaqueDesc, FilterMode.Bilinear);
            cmd.CopyTexture(source, tempCopy);

            if (kernelSize == 2)
                material.DisableKeyword(kernelKeyword);
            else
                material.EnableKeyword(kernelKeyword);

            material.SetFloat(blurAmountString, blurAmount);

            if (blurMask != null || previous != blurMask)
            {
                previous = blurMask;
                material.SetTexture(maskTextureString, blurMask);
            }


            if (blurAmount == 0)
            {
                cmd.Blit(tempCopy, source);
                context.ExecuteCommandBuffer(cmd);
                CommandBufferPool.Release(cmd);
                return;
            }
            else if (numberOfPasses == 1)
            {
                cmd.GetTemporaryRT(blurTexString, Screen.width / 2, Screen.height / 2, 0, FilterMode.Bilinear);
                cmd.Blit(tempCopy, blurTex, material, 0);
            }
            else if (numberOfPasses == 2)
            {
                cmd.GetTemporaryRT(blurTexString, Screen.width / 2, Screen.height / 2, 0, FilterMode.Bilinear);
                cmd.GetTemporaryRT(blurTempString, Screen.width / 4, Screen.height / 4, 0, FilterMode.Bilinear);
                cmd.Blit(tempCopy, blurTemp, material, 0);
                cmd.Blit(blurTemp, blurTex, material, 0);
            }
            else if (numberOfPasses == 3)
            {
                cmd.GetTemporaryRT(blurTexString, Screen.width / 4, Screen.height / 4, 0, FilterMode.Bilinear);
                cmd.GetTemporaryRT(blurTempString, Screen.width / 8, Screen.height / 8, 0, FilterMode.Bilinear);
                cmd.Blit(tempCopy, blurTex, material, 0);
                cmd.Blit(blurTex, blurTemp, material, 0);
                cmd.Blit(blurTemp, blurTex, material, 0);
            }
            else if (numberOfPasses == 4)
            {
                cmd.GetTemporaryRT(blurTexString, Screen.width / 4, Screen.height / 4, 0, FilterMode.Bilinear);
                cmd.GetTemporaryRT(blurTempString, Screen.width / 8, Screen.height / 8, 0, FilterMode.Bilinear);
                cmd.GetTemporaryRT(blurTemp1String, Screen.width / 16, Screen.height / 16, 0, FilterMode.Bilinear);
                cmd.Blit(tempCopy, blurTemp, material, 0);
                cmd.Blit(blurTemp, blurTemp1, material, 0);
                cmd.Blit(blurTemp1, blurTemp, material, 0);
                cmd.Blit(blurTemp, blurTex, material, 0);
            }
            else if (numberOfPasses == 5)
            {
                cmd.GetTemporaryRT(blurTexString, Screen.width / 4, Screen.height / 4, 0, FilterMode.Bilinear);
                cmd.GetTemporaryRT(blurTempString, Screen.width / 8, Screen.height / 8, 0, FilterMode.Bilinear);
                cmd.GetTemporaryRT(blurTemp1String, Screen.width / 16, Screen.height / 16, 0, FilterMode.Bilinear);
                cmd.Blit(tempCopy, blurTex, material, 0);
                cmd.Blit(blurTex, blurTemp, material, 0);
                cmd.Blit(blurTemp, blurTemp1, material, 0);
                cmd.Blit(blurTemp1, blurTemp, material, 0);
                cmd.Blit(blurTemp, blurTex, material, 0);
            }

            cmd.Blit(tempCopy, source, material, 1);
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(blurTemp1String);
            cmd.ReleaseTemporaryRT(tempCopyString);
            cmd.ReleaseTemporaryRT(blurTexString);
            cmd.ReleaseTemporaryRT(blurTempString);
        }
    }
}
