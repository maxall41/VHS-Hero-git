using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeFlip : MonoBehaviour
{
	private bool m_FacingRight = true;

    public void DeFlip_F()
    {
        Flip();
    }

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
