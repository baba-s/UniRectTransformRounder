﻿using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
	/// <summary>
	/// RectTransform のパラメータを四捨五入するコマンドをコンテキストメニューに追加するエディタ拡張
	/// </summary>
	internal static class RectTransformRounder
	{
		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// RectTransform のパラメータを四捨五入します
		/// </summary>
		[MenuItem( "CONTEXT/RectTransform/Round" )]
		private static void Round( MenuCommand command )
		{
			var t = ( RectTransform ) command.context;

			Undo.RecordObject( t, "Round" );

			RoundImpl( t );
		}

		/// <summary>
		/// 子オブジェクトも含む RectTransform のパラメータを四捨五入します
		/// </summary>
		[MenuItem( "CONTEXT/RectTransform/Round In Children" )]
		private static void RoundInChildren( MenuCommand command )
		{
			var t        = ( RectTransform ) command.context;
			var children = t.GetComponentsInChildren<RectTransform>();

			Undo.RecordObjects( children, "Round In Children" );

			foreach ( var n in children )
			{
				var gameObject             = n.gameObject;
				var isPartOfPrefabInstance = PrefabUtility.IsPartOfPrefabInstance( gameObject );
				var isPrefabInstanceRoot   = PrefabUtility.IsAnyPrefabInstanceRoot( gameObject );

				// 通常のシーンの Hierarchy において
				// プレハブのインスタンスのルートであれば四捨五入の対象
				// プレハブのインスタンスの子であれば対象外
				if ( isPartOfPrefabInstance && !isPrefabInstanceRoot ) continue;

				RoundImpl( n );
			}
		}

		/// <summary>
		/// RectTransform のパラメータを四捨五入します
		/// </summary>
		private static void RoundImpl( RectTransform t )
		{
			var anchoredPosition3D = t.anchoredPosition3D;
			anchoredPosition3D.x = Mathf.Round( anchoredPosition3D.x );
			anchoredPosition3D.y = Mathf.Round( anchoredPosition3D.y );
			anchoredPosition3D.z = Mathf.Round( anchoredPosition3D.z );
			t.anchoredPosition3D = anchoredPosition3D;

			var sizeDelta = t.sizeDelta;
			sizeDelta.x = Mathf.Round( sizeDelta.x );
			sizeDelta.y = Mathf.Round( sizeDelta.y );
			t.sizeDelta = sizeDelta;

			var offsetMin = t.offsetMin;
			offsetMin.x = Mathf.Round( offsetMin.x );
			offsetMin.y = Mathf.Round( offsetMin.y );
			t.offsetMin = offsetMin;

			var offsetMax = t.offsetMax;
			offsetMax.x = Mathf.Round( offsetMax.x );
			offsetMax.y = Mathf.Round( offsetMax.y );
			t.offsetMax = offsetMax;

			var localScale = t.localScale;
			localScale.x = Mathf.Round( localScale.x );
			localScale.y = Mathf.Round( localScale.y );
			localScale.z = Mathf.Round( localScale.z );
			t.localScale = localScale;
		}
	}
}