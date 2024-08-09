using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTING
{
    public class Testing_Architect : MonoBehaviour
    {
        DialogueSystem ds;
        TextArchitect architect;

        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instant;

        string[] lines = new string[6]
        {
            "你就是教授派下来的助手？",
            "那你去帮我泡一杯咖啡吧。",
            "“好吧，让我看有什么简单的案子吧。",
            "嗯？那我看看。",
            "行吧，我们出发吧",
            "行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行行"
        };

        // Start is called before the first frame update
        void Start()
        {
            ds = DialogueSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            architect.buildMethod = TextArchitect.BuildMethod.fade;
            ///architect.speed = 0.5f;
        }

        // Update is called once per frame
        void Update()
        {
            if (bm != architect. buildMethod)
            {
                architect. buildMethod = bm;
                architect. Stop();
            }

            if (Input.GetKeyDown (KeyCode.S))
                architect.Stop();

            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (architect.isBuilding)
                {
                    if (!architect.hurryUp)
                        architect.hurryUp = true;
                    else
                        architect.ForceComplete();
                }
                else
                    architect.Build(lines[Random.Range(0,lines.Length)]);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                architect.Append(lines[Random.Range(0,lines.Length)]);
            }
        }
    }
}
