using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TestObserver : Observer
{
    [Observing("TestStore")] string testText;

    public Text text;
    void Start() {
        text.text = this.testText;
        TestStore.Instance.Set<string>("testText", "new val");
    }

    void Update() {
        this.text.text = testText;
    }
}