using System.Activities;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;

using AM.Activities.Common.BaseActivities;

namespace AM.Activities.Example
{
    public class TextToSpeechActivity : AbstractTaskAsyncCodeActivity
    {
        public InArgument<string> Text { get; set; }

        protected override Task ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            string text = Text.Get(context);

            return Task.Run(() =>
            {
                using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
                {
                    synthesizer.SetOutputToDefaultAudioDevice();
                    synthesizer.Speak(text);
                }
            }, cancellationToken);
        }
    }
}