using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ObjectStorage {

	public const string ALL = "*";
	
	public ObjectStorage() {
	}
	
	public abstract string update(Object o, string category, string id);
	
	public abstract Object load(string category, string id);
	
	public abstract List<Object> loadAll(string category);
	
	public abstract List<string> list(string category);
	
	public abstract bool delete(string category, string id);
	
}