CREATE VIEW plm_vwAllInformationPests
AS 
SELECT C.CropId, C.CropName, S.StateId, S.StateName, P.PestId, P.PestName, P.ScientificName, CFP.FightTypeId, FT.FightName, 
				CFP.ActiveSubstanceId, ASS.ActiveSubstanceName, CFP.Description AS Treatment
	FROM Pests P 
	INNER JOIN CropPests CP ON CP.PestId = P.PestId
	INNER JOIN CropFightPests CFP ON CFP.CropId = CP.CropId AND
			                         CFP.StateId = CP.StateId AND
			                         CFP.PestId = CP.PestId
	INNER JOIN FightTypes FT ON FT.FightTypeId = CFP.FightTypeId
	INNER JOIN Crops C ON CP.CropId = C.CropId
	INNER JOIN StateCrops SC ON CP.CropId = SC.CropId AND 
								CP.StateId = SC.StateId
	INNER JOIN States S ON SC.StateId = S.StateId
	INNER JOIN ActiveSubstances ASS ON CFP.ActiveSubstanceId = ASS.ActiveSubstanceId


CREATE VIEW plm_vwAllInformationDiseases
AS 	
SELECT C.CropId, C.CropName, S.StateId, S.StateName, D.DiseaseId, D.DiseaseName, D.ScientificName, CFD.FightTypeId, FT.FightName, 
				CFD.ActiveSubstanceId, ASS.ActiveSubstanceName, CFD.Description AS Treatment
	FROM Diseases D 
	INNER JOIN CropDiseases CD ON CD.DiseaseId = D.DiseaseId 
	INNER JOIN CropFightDiseases CFD ON CFD.CropId = CD.CropId AND
			                         CFD.StateId = CD.StateId AND
			                         CFD.DiseaseId = CD.DiseaseId
	INNER JOIN FightTypes FT ON FT.FightTypeId = CFD.FightTypeId
	INNER JOIN Crops C ON CD.CropId = C.CropId
	INNER JOIN StateCrops SC ON CD.CropId = SC.CropId AND
								CD.StateId = SC.StateId
	INNER JOIN States S ON SC.StateId = S.StateId
	INNER JOIN ActiveSubstances ASS ON CFD.ActiveSubstanceId = ASS.ActiveSubstanceId


CREATE VIEW plm_vwAllInformationWeeds
AS 	
SELECT C.CropId, C.CropName, S.StateId, S.StateName, W.WeedId, W.WeedName, W.ScientificName,W.WeedTypeId,WT.TypeName, CFW.FightTypeId, FT.FightName, 
				CFW.ActiveSubstanceId, ASS.ActiveSubstanceName, CFW.Description AS Treatment
	FROM Weeds W 
	INNER JOIN CropWeeds CW ON CW.WeedId = W.WeedId
	INNER JOIN CropFightWeeds CFW ON CFW.CropId = CW.CropId AND
			                         CFW.StateId = CW.StateId AND
			                         CFW.WeedId = CW.WeedId
	INNER JOIN FightTypes FT ON FT.FightTypeId = CFW.FightTypeId
	INNER JOIN Crops C ON CW.CropId = C.CropId
	INNER JOIN StateCrops SC ON CW.CropId = SC.CropId AND
								CW.StateId = SC.StateId
	INNER JOIN States S ON SC.StateId = S.StateId
	INNER JOIN ActiveSubstances ASS ON CFW.ActiveSubstanceId = ASS.ActiveSubstanceId
	INNER JOIN WeedTypes WT ON W.WeedTypeId = WT.WeedTypeId
