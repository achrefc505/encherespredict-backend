-- ============================================================
-- EnchèresPredict — Scripts de gestion utilisateurs
-- Base : EncheresPredictDb (LocalDB dev / Azure SQL prod)
-- ============================================================

-- 1. Lister tous les utilisateurs
SELECT
    u.Email,
    u.FirstName,
    u.LastName,
    u.IsAdmin,
    u.BetaExpiresAt,
    DATEDIFF(day, GETUTCDATE(), u.BetaExpiresAt) AS DaysLeft,
    u.CreatedAt
FROM AspNetUsers u
ORDER BY u.CreatedAt DESC;


-- 2. Voir les rôles de chaque utilisateur
SELECT
    u.Email,
    r.Name AS Role
FROM AspNetUsers u
JOIN AspNetUserRoles ur ON u.Id = ur.UserId
JOIN AspNetRoles r ON ur.RoleId = r.Id
ORDER BY u.Email;


-- 3. Prolonger l'accès beta d'un utilisateur (+30 jours)
UPDATE AspNetUsers
SET BetaExpiresAt = DATEADD(day, 30, GETUTCDATE())
WHERE Email = 'utilisateur@exemple.fr';


-- 4. Révoquer immédiatement l'accès beta d'un utilisateur
UPDATE AspNetUsers
SET BetaExpiresAt = DATEADD(day, -1, GETUTCDATE())
WHERE Email = 'utilisateur@exemple.fr';


-- 5. Liste des beta-testeurs dont l'accès expire dans < 7 jours (à relancer)
SELECT
    Email,
    FirstName,
    LastName,
    BetaExpiresAt,
    DATEDIFF(day, GETUTCDATE(), BetaExpiresAt) AS DaysLeft
FROM AspNetUsers
WHERE IsAdmin = 0
  AND BetaExpiresAt BETWEEN GETUTCDATE() AND DATEADD(day, 7, GETUTCDATE())
ORDER BY BetaExpiresAt;


-- 6. Stats rapides beta
SELECT
    COUNT(*) AS TotalUsers,
    SUM(CASE WHEN IsAdmin = 1 THEN 1 ELSE 0 END) AS Admins,
    SUM(CASE WHEN IsAdmin = 0 AND BetaExpiresAt > GETUTCDATE() THEN 1 ELSE 0 END) AS BetaActifs,
    SUM(CASE WHEN IsAdmin = 0 AND BetaExpiresAt <= GETUTCDATE() THEN 1 ELSE 0 END) AS BetaExpires
FROM AspNetUsers;
