using UnityEngine;
using System.Collections;

public class GameEvent {

    public string payload;
    public string type;
    public string initiator;

	public GameEvent(string payload, string type, string initiator) {
        this.payload = payload;
        this.type = type;
        this.initiator = initiator;
	}
	
}