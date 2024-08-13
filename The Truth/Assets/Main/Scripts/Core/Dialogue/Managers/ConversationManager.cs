using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DIALOGUE
{
    public class ConversationManager
    {
        private DialogueSystem dialogueSystem => DialogueSystem.instance;

        private Coroutine process = null;
        public bool isRunning => process != null;

        private TextArchitect architect = null;

        public ConversationManager(TextArchitect architect)
        {
            this.architect = architect;
        }
        
        public void StartConversation(List<string> conversation)
        {
            StopConversation();

            process = dialogueSystem.StartCoroutine(RunningConversation(conversation));
        }

        public void StopConversation()
        {
            if (!isRunning)
                return;

            dialogueSystem.StopCoroutine(process);
            process = null;
        }

        IEnumerator RunningConversation(List<string> conversation)
        {
            for(int i = 0; i < conversation.Count; i++)
            {
                // Dont show any blank lines or try to run any logic on them.
                if (string.IsNullOrWhiteSpace(conversation[i]))
                    continue;
                
                DIALOGUE_LINE line = DialogueParser.Parse(conversation[i]);

                //show dialogue
                if (line.hasDialogue)
                    yield return Line_RunDialogue(line);

                //run any commands
                if (line.hasCommand)
                    yield return Line_RunCommands(line);

                yield return new WaitForSeconds(1);
            }
        }

        IEnumerator Line_RunDialogue(DIALOGUE_LINE line)
        {
            //show or hide speaker name if there is one present.
            if (line.hasSpeaker)
                dialogueSystem.ShowSpeakerName(line.speaker);
            else
                dialogueSystem.HideSpeakerName();

            //Build dialogue
            architect.Build(line.dialogue);

            while(architect.isBuilding)
                yield return null;
        }

        IEnumerator Line_RunCommands(DIALOGUE_LINE line)
        {
            Debug.Log(line.commands);
            yield return null;
        }
    }
}