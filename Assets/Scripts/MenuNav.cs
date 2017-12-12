using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuNav : BaseInputModule
{

	public float Cooldown = .2f;
	public GameObject[] Selectable;

	float deadzone = 0.1f;
	int Selected = 0;
	float Cooling = 0;

	public override void Process()
	{
		Cooling -= Time.deltaTime;
		if (InputHelper.GetStick(0).y > deadzone && Cooling <= 0)
			Select(-1);
		else if (InputHelper.GetStick(0).y < -deadzone && Cooling <= 0)
			Select(1);
		else
			eventSystem.SetSelectedGameObject(Selectable[Selected], new BaseEventData(eventSystem));

		if (InputHelper.GetActionDown(0, Joycon.Button.HOME))
			ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);
	}

	void Select(int i)
	{
		Selected += i;
		if (Selected > 3)
			Selected = 0;
		else if (Selected < 0)
			Selected = 3;

		eventSystem.SetSelectedGameObject(Selectable[Selected], new BaseEventData(eventSystem));
		Cooling = Cooldown;
	}
}
