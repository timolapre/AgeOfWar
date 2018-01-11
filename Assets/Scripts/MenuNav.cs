using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices;

public class MenuNav : PointerInputModule
{
	public float Cooldown = .2f;

	public static GameObject Selected;
    public GameObject tset;
	float deadzone = 0.5f;
	float Cooling = 0;
	Vector3 LastMouse;
	int inputmode = 0;
	PointerEventData p;
	bool canMouse = true;
	GameObject Dragging;
	GameObject MouseDown;

	[DllImport("user32.dll", EntryPoint = "SetCursorPos")]
	private static extern bool SetCursorPos(int X, int Y);
	private static bool SetCursorPos(Vector2 pos)
	{
		return SetCursorPos((int)pos.x, (int)pos.y);
	}

	protected override void Start()
	{
		Selected = eventSystem.firstSelectedGameObject;
	}

	public override void Process()
	{
        tset = Selected;
		Cooling -= Time.deltaTime;
        
		if ((Mathf.Abs(InputHelper.GetStick(0).y) > deadzone || Mathf.Abs(InputHelper.GetStick(0).x) > deadzone) && Cooling <= 0)
		{
			if (inputmode != 0)
				inputmode = 0;
			else
				Select(InputHelper.GetStick(0));
			Cooling = Cooldown;
		}
		else if (inputmode == 0)
			Select(Vector2.zero);


		if ((Input.GetMouseButtonUp(0) || LastMouse != Input.mousePosition) && inputmode != 1 && canMouse)
		{
			Cursor.lockState = CursorLockMode.Locked; Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			inputmode = 1;
		}

		MouseState ms = GetMousePointerEventData();
		base.GetPointerData(-1, out p, true);
		if (inputmode == 1)
		{
			if (p.pointerCurrentRaycast.gameObject != null)
			{
				Selected = p.pointerCurrentRaycast.gameObject.GetComponentInParent<UnityEngine.UI.Selectable>().gameObject;
				Select(Vector2.zero);
				if (Selected.GetComponent<Slider>() != null && Input.GetMouseButtonDown(0))
					Dragging = Selected;

				if (Input.GetMouseButtonDown(0))
					MouseDown = Selected;
				else if (Input.GetMouseButtonUp(0) && MouseDown == Selected)
					ExecuteEvents.Execute(Selected, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);
			}
			else
				eventSystem.SetSelectedGameObject(null, new BaseEventData(eventSystem));

			if (Input.GetMouseButtonUp(0))
				MouseDown = null;
			if (Dragging != null)
			{
				Dragging.GetComponent<Slider>().OnDrag(p);
				if (Input.GetMouseButtonUp(0))
					Dragging = null;
			}
		}
		else if(Cursor.visible)
		{
			SetCursorPos(0, 0);
			Cursor.visible = false;
			canMouse = false;
		}
		else if(canMouse == false)
			canMouse = true;


		if (InputHelper.GetActionDown(0, Joycon.Button.HOME))
			ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);

		LastMouse = Input.mousePosition;
	}

	void Select(Vector2 dir)
	{
		MoveDirection md = DetermineMoveDirection(dir.x * 10, dir.y * 10);
		try
		{
			if (md == MoveDirection.Up)
				Selected = Selected.GetComponent<UnityEngine.UI.Selectable>().FindSelectableOnUp().gameObject;
			else if (md == MoveDirection.Down)
				Selected = Selected.GetComponent<UnityEngine.UI.Selectable>().FindSelectableOnDown().gameObject;
			else if (md == MoveDirection.Left)
				if (Selected.GetComponent<Slider>() != null)
					Selected.GetComponent<Slider>().value -= .05f;
				else
					Selected = Selected.GetComponent<UnityEngine.UI.Selectable>().FindSelectableOnLeft().gameObject;
			else if (md == MoveDirection.Right)
				if (Selected.GetComponent<Slider>() != null)
					Selected.GetComponent<Slider>().value += .05f;
				else
					Selected = Selected.GetComponent<UnityEngine.UI.Selectable>().FindSelectableOnRight().gameObject;
		}
		catch (NullReferenceException) { }

		if(Selected != null)
			eventSystem.SetSelectedGameObject(Selected, new BaseEventData(eventSystem));
	}

    public void ResetSelected(GameObject obj)
    {
        Selected = obj;
        Debug.Log(obj.name);
    }
}
