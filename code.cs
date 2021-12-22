using System;
using System.Collections;
using UnityEngine;

// Token: 0x020008A0 RID: 2208
public static partial class GameOptionsManager
{
	// Token: 0x06005F8F RID: 24463
	public static void ApplyTextureFilter()
	{
		int textureFilter = GameOptionsManager.GetTextureFilter();
		QualitySettings.anisotropicFiltering = ((textureFilter == 0) ? AnisotropicFiltering.ForceEnable : ((textureFilter <= 3) ? AnisotropicFiltering.ForceEnable : AnisotropicFiltering.ForceEnable));
		if (GameOptionsManager.TextureFilterChanged != null)
		{
			GameOptionsManager.TextureFilterChanged(textureFilter);
		}
		Log.Out("ApplyTextureFilter {0}, AF {1}", new object[]
		{
			textureFilter,
			QualitySettings.anisotropicFiltering
		});
	}
}

___________________________________________

using System;
using System.Collections;
using UnityEngine;

// Token: 0x020008A0 RID: 2208
public static partial class GameOptionsManager
{
	// Token: 0x06005F94 RID: 24468
	private static void ResetGraphicsOptions()
	{
		GamePrefs.Set(EnumGamePrefs.OptionsGfxResolution, (int)GamePrefs.GetDefault(EnumGamePrefs.OptionsGfxResolution));
		GamePrefs.Set(EnumGamePrefs.OptionsGfxDynamicMode, (int)GamePrefs.GetDefault(EnumGamePrefs.OptionsGfxDynamicMode));
		GamePrefs.Set(EnumGamePrefs.OptionsGfxDynamicScale, (float)GamePrefs.GetDefault(EnumGamePrefs.OptionsGfxDynamicScale));
		GamePrefs.Set(EnumGamePrefs.OptionsGfxBrightness, (float)GamePrefs.GetDefault(EnumGamePrefs.OptionsGfxBrightness));
		GamePrefs.Set(EnumGamePrefs.OptionsGfxVsync, (int)GamePrefs.GetDefault(EnumGamePrefs.OptionsGfxVsync));
		int value = 2;
		float num = (float)SystemInfo.systemMemorySize;
		float num2 = (float)SystemInfo.graphicsMemorySize;
		if (num2 < 128f || num < 128f)
		{
			value = 1;
		}
		if (num2 > 128f && num > 128f)
		{
			string text = SystemInfo.graphicsDeviceVendor.ToLower();
			if (text.Contains("nvidia"))
			{
				if (!GameOptionsManager.FindGfxName(" 1070"))
				{
					value = 3;
					if (GameOptionsManager.FindGfxName(" 208, 307, 308, 309"))
					{
						value = 4;
					}
				}
			}
			else if ((text == "amd" || text == "ati") && !GameOptionsManager.FindGfxName("RX 580,RX 590,RX 5500"))
			{
				value = 3;
				if (GameOptionsManager.FindGfxName(" 680, 690"))
				{
					value = 4;
				}
			}
		}
		GamePrefs.Set(EnumGamePrefs.OptionsGfxQualityPreset, value);
		GameOptionsManager.SetGraphicsQuality();
		GamePrefs.Set(EnumGamePrefs.DynamicMeshEnabled, (bool)GamePrefs.GetDefault(EnumGamePrefs.DynamicMeshEnabled));
		GamePrefs.Set(EnumGamePrefs.DynamicMeshDistance, (int)GamePrefs.GetDefault(EnumGamePrefs.DynamicMeshDistance));
		GamePrefs.Set(EnumGamePrefs.NoGraphicsMode, (bool)GamePrefs.GetDefault(EnumGamePrefs.NoGraphicsMode));
	}
}

___________________________________________

using System;
using System.Collections;
using UnityEngine;

// Token: 0x020008A0 RID: 2208
public static partial class GameOptionsManager
{
	// Token: 0x06005F8E RID: 24462
	public static int GetTextureFilter()
	{
		int result = GamePrefs.GetInt(EnumGamePrefs.OptionsGfxTexFilter);
		if ((float)SystemInfo.graphicsMemorySize < 128f)
		{
			result = 0;
		}
		return result;
	}
}

