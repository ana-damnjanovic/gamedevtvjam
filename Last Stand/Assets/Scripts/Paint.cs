using UnityEngine;
using System.Collections;

// from https://gist.github.com/huytd/9569548
public class Paint : MonoBehaviour
{
	[SerializeField]
	private Renderer m_rendererToPaintOn;

	[SerializeField]
	private Color m_paintColor = Color.black;

	private Texture2D m_tex;
	private Material m_material;
	void Start()
	{
		m_tex = new Texture2D(128, 128);
		m_material = new Material(Shader.Find("Specular"));
		m_rendererToPaintOn.sharedMaterial = m_material;
		m_rendererToPaintOn.sharedMaterial.mainTexture = m_tex;
	}

	void Update()
	{
		if (m_tex != null)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Input.GetButton("Fire1"))
			{
				if (Physics.Raycast(ray, out hit, Mathf.Infinity))
				{
					// Find the u,v coordinate of the Texture
					Vector2 uv;
					uv.x = (hit.point.x - hit.collider.bounds.min.x) / hit.collider.bounds.size.x;
					uv.y = (hit.point.y - hit.collider.bounds.min.y) / hit.collider.bounds.size.y;
					// Paint it
					m_tex.SetPixel((int)(-uv.x * m_tex.width), (int)(uv.y * m_tex.height), m_paintColor);
					m_tex.SetPixel((int)(-uv.x * m_tex.width), (int)(uv.y * m_tex.height) + 1, m_paintColor);
					m_tex.SetPixel((int)(-uv.x * m_tex.width) + 1, (int)(uv.y * m_tex.height), m_paintColor);
					m_tex.SetPixel((int)(-uv.x * m_tex.width), (int)(uv.y * m_tex.height) - 1, m_paintColor);
					m_tex.SetPixel((int)(-uv.x * m_tex.width) - 1, (int)(uv.y * m_tex.height), m_paintColor);
					m_tex.SetPixel((int)(-uv.x * m_tex.width) + 1, (int)(uv.y * m_tex.height) + 1, m_paintColor);
					m_tex.SetPixel((int)(-uv.x * m_tex.width) - 1, (int)(uv.y * m_tex.height) - 1, m_paintColor);
					m_tex.SetPixel((int)(-uv.x * m_tex.width) - 1, (int)(uv.y * m_tex.height) + 1, m_paintColor);
					m_tex.SetPixel((int)(-uv.x * m_tex.width) + 1, (int)(uv.y * m_tex.height) - 1, m_paintColor);
					m_tex.Apply();
				}
			}
		}
	}
}