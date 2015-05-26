﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuadNode  {

	private Body body = null;
	private Body quadBody = null;

	private Vector3 center;
	private float size;

	private QuadNode nw;
	private QuadNode ne;
	private QuadNode sw;
	private QuadNode se;

	public QuadNode(Vector3 center, float size){
		this.center = center;
		this.size = size ;
	}

	public void addBody(Body body){
		if(!contains(body)){
			return;
		}
		if(this.body == null){
			this.body = body;
			return;

		}
		createChildNodeifNeeded();
		nw.addBody(body);
		ne.addBody(body);
		sw.addBody(body);
		se.addBody(body);

		nw.addBody(this.body);
		ne.addBody(this.body);
		sw.addBody(this.body);
		se.addBody(this.body);


		if(quadBody==null){
			quadBody = new Body(null);
			quadBody.position = body.position;
		}else{
			quadBody.addBody(body);
		}
	}

	public bool contains(Body body){
		if(body.position.x >= center.x +(size/2f))
			return false;
		if(body.position.x <= center.x -(size/2f))
			return false;
		if(body.position.y >= center.y +(size/2f))
			return false;
		if(body.position.y <= center.y -(size/2f))
			return false;
		return true;
	}

	private void createChildNodeifNeeded(){
		if(nw !=null)
			return;
		float newSize = size/2f;
		nw = new QuadNode(new Vector3(center.x-newSize/2f,center.y+newSize/2f,0f),newSize);
		ne = new QuadNode(new Vector3(center.x+newSize/2f,center.y+newSize/2f,0f),newSize);
		sw = new QuadNode(new Vector3(center.x-newSize/2f,center.y-newSize/2f,0f),newSize);
		se = new QuadNode(new Vector3(center.x+newSize/2f,center.y-newSize/2f,0f),newSize);
	}

	public void getAllQuad(List<Quad> quads){
		quads.Add(new Quad(center,size));
		if(nw == null)
			return;
		nw.getAllQuad(quads);
		ne.getAllQuad(quads);
		sw.getAllQuad(quads);
		se.getAllQuad(quads);

	}

}
