using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chars {
	public string meme;
	public Stats stat;
	public List<Skill> skills = new List<Skill> ();
	public Tag[] tags;
	public Status[] statuses;
	public int pos;

	public Chars() {
	}
	public Chars(string meme, Stats stat, Tag[] tags, Status[] statuses) {
		this.meme = meme;
		this.stat = stat;
		this.tags = tags;
		this.statuses = statuses;
		this.pos = 0;
	}
}